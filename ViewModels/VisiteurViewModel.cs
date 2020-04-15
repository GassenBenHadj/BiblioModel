using BiblioModel.DAL.Interfaces;
using BiblioModel.Models;
using System;
using System.Collections.Generic;

namespace BiblioModel.ViewModels
{
    public class VisiteurViewModel
    {
        IVisiteurRepository _repository;

        public VisiteurViewModel(IVisiteurRepository repository)
        {
            _repository = repository;
            
            
        }
        public void Add(Visiteur visiteur)=>_repository.Create(visiteur);

        public void Delete(Visiteur visiteur)=>  _repository.Delete(visiteur);
        
        public void Update(Visiteur visiteur)=>_repository.Update(visiteur);
        
        public Visiteur Find(int id)=> _repository.Find(id).Result;

        public int Count() => _repository.Count().Result;

        public IAsyncEnumerable<Visiteur> FindAll(Func<Visiteur, bool> predicate) => 
                                            _repository.FindAll(predicate);
        public IAsyncEnumerable<Visiteur> GetAll() =>
                                             _repository.GetAll();
        
    }
}
