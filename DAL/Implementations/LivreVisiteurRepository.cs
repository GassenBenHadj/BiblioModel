using BiblioModel.DAL.Interfaces;
using BiblioModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BiblioModel.DAL.Implementations
{
    public class LivreVisiteurRepository :Repository<RelationLivreVisiteur>, ILivreVisitieurRepository
    {
        public LivreVisiteurRepository(BiblioDBContext context):base(context)
        {

        }

       
		#region CRUD Operations
        async public override Task<int> Count()
		{
			var result = await Task.Factory.StartNew(
				()=> Context.Set<RelationLivreVisiteur>().Count());
			return result;
		}

		public override void Create(RelationLivreVisiteur entity)
		{
			Context.Add(entity);
			Save();
		}

		public override void Delete(RelationLivreVisiteur entity)
		{
			Context.Remove(entity);
			Save();
		}

		public override Task<RelationLivreVisiteur> Find(int id)
		{
		   var result = Task.Factory.StartNew(
			   ()=> Context.RelationLivreVisiteurs.Find(id));
			return result;
		}


		public override IAsyncEnumerable<RelationLivreVisiteur> GetAll()
		{
			return Context.RelationLivreVisiteurs 
				as IAsyncEnumerable<RelationLivreVisiteur>;
		}

		/*public override RelationLivreVisiteur GetById(int id)
		{
			return Context.RelationLivreVisiteurs.Find(id);
		}*/
		public override void Update(RelationLivreVisiteur entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
			Save();
		}
	}
    #endregion
}
