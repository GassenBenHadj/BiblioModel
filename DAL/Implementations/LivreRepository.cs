using BiblioModel.DAL.Interfaces;
using BiblioModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioModel.DAL.Implementations
{
    public class LivreRepository : Repository<Livre>, ILivreRepository
    {

		public LivreRepository(BiblioDBContext context):base(context)
		{

		}

		async public IAsyncEnumerable<Livre> GetLivreByPage(int index,int size)
		{

			var result = await GetAll().ToArrayAsync();
			for (int i = index-1; i < size; i++)
			{
				yield return result[i];
			}
		}

		async public IAsyncEnumerable<Livre> GetLivreWithAuteurs(Func<Livre, bool> predicate)
		{
			var result = await Task.Factory.StartNew(
				() => Context.Set<Livre>()
				.Include(v => v.RelationLivreVisiteurs)
				.OrderBy(l => l.Titre)
				.Where(predicate) as IAsyncEnumerable<Livre>);
			await foreach (var item in result)
			{
				yield return item;
			}
		}

		async public IAsyncEnumerable<Livre> GetTopLivres(int count)
		{
			var result = await GetAll().ToArrayAsync();
			for (int i = 0; i < count; i++)
			{
				yield return result[i];
			}

			#region old code
			/*var result = await Task.Factory.StartNew(
				() => Context.Set<Livre>()
				.OrderByDescending(l=>l.Prix)
				as IAsyncEnumerable<Livre>);
			await foreach (var item in result)
			{
				yield return item;
			}*/
			#endregion
		}



        #region CRUD Operations   
        async public override Task<int> Count()
		{
			var result= await Task.Factory.StartNew(()=>
			Context.Set<Livre>().Count());
			return result;
		}

		public override void Create(Livre entity)
		{
			Context.Add(entity);
			Save();
		}

		public override void Delete(Livre entity)
		{
			Context.Remove(entity);
			Save();
		}

		public override Task<Livre> Find(int id)
		{
			var result = Task.Factory.StartNew(()=> Context.Livres.Find(id));
			return result;
		}

		/* Mode synchrone*/
		public override IAsyncEnumerable<Livre> GetAll()
		{
			return Context.Livres as IAsyncEnumerable<Livre>;
		}
			
		public override void Update(Livre entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
			Save();
		}
        #endregion

    }
}
