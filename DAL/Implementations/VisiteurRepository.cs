using BiblioModel.DAL.Interfaces;
using BiblioModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioModel.DAL.Implementations
{
	public class VisitieurRepository : Repository<Visiteur>, IVisiteurRepository
	{
		public VisitieurRepository(BiblioDBContext biblioDBContext)
			:base(biblioDBContext)
		{

		}

		async public IAsyncEnumerable<Visiteur> GetTopAuteurs(int count)
		{
			var result = await GetAll().ToArrayAsync();
			for (int i = 0; i < count; i++)
			{
				yield return result[i];
			}

			#region old code
			/*var result = await Task.Factory.StartNew(
				() => Context.Set<Auteur>()
				.Where(v=>v.Discriminator=="Auteur")
				.OrderByDescending(a => a.RelationLivreVisiteurs.Count())
				as IAsyncEnumerable<Auteur>);
			await foreach (var item in result)
			{
				yield return item;
			}*/
			#endregion
		}

        async public IAsyncEnumerable<Visiteur> GetAuteurWithLivres(Func<Visiteur, bool> predicate)
		{
			var result = await Task.Factory.StartNew(
				() => Context.Set<Visiteur>()
				.Include(v => v.RelationLivreVisiteurs)
				.OrderBy(v=>v.Nom)
				.Where(predicate) as IAsyncEnumerable<Visiteur>);
			await foreach (var item in result)
			{
				yield return item;
			}
		}

		async public IAsyncEnumerable<Visiteur> GetVisiteurByPage(int index, int size, Func<Visiteur, bool> predicate)
		{
			var result = await GetAll().ToArrayAsync();
			for (int i = index-1; i < size; i++)
			{
				yield return result[i];
			}

            #region old code
            /*var result = await Task.Factory.StartNew(
				() => Context.Set<Visiteur>()
				.OrderBy(l => l.Nom)
				.Where(predicate)
				.Skip((index - 1) * size) as IAsyncEnumerable<Visiteur>);
			await foreach (var item in result)
			{
				yield return item;
			}*/
            #endregion
        }
        #region CRUD Operations
        async public override Task<int> Count()
		{
			var result = await Task.Factory.StartNew(
				()=> Context.Set<Visiteur>().Count());
			return result;
		}

		public override void Create(Visiteur entity)
		{
			Context.Add(entity);
			Save();
		}

		public override void Delete(Visiteur entity)
		{
			Context.Remove(entity);
			Save();
		}

		async public override Task<Visiteur> Find(int id)
		{
			var result = await Task.Factory.StartNew(
				()=> Context.Visiteurs.Find(id));
			return result;
		}

		public override IAsyncEnumerable<Visiteur> FindAll(Func<Visiteur, bool> predicate)
		{
			IAsyncEnumerable<Visiteur> visiteurs  =  base.FindAll(predicate);
			return visiteurs;
		}

		public override IAsyncEnumerable<Visiteur> GetAll()
		{
			return Context.Visiteurs as IAsyncEnumerable<Visiteur>;
		}

		
		public override void Update(Visiteur entity)
		{
			Context.SaveChanges();
		}
        #endregion region
    }
}

