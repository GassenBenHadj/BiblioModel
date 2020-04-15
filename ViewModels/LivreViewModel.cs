using BiblioModel.DAL.Interfaces;
using BiblioModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioModel.ViewModels
{
    public class LivreViewModel
    {
        ILivreRepository _repository;

        public LivreViewModel(ILivreRepository repository)
        {
            _repository = repository;
            
        }
        public void Add(Livre livre) => _repository.Create(livre);

        public void Delete(Livre livre) => _repository.Delete(livre);

        public void Update(Livre livre) => _repository.Update(livre);

        public Livre Find(int id) => _repository.Find(id).Result;

        public int Count() => _repository.Count().Result;

        public IAsyncEnumerable<Livre> FindAll(Func<Livre, bool> predicate) =>
                                            _repository.FindAll(predicate);
        public IAsyncEnumerable<Livre> GetAll() =>
                                             _repository.GetAll();

        public IAsyncEnumerable<Livre> GetLivreByPage(int index, int size)
                            => _repository.GetLivreByPage(index, size);

        public IAsyncEnumerable<Livre> GetLivreWithAuteurs(Func<Livre,bool> predicate)
                            => _repository.GetLivreWithAuteurs(predicate);

    }
}
