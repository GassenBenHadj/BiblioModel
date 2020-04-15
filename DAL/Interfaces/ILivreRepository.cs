using BiblioModel.Models;
using System;
using System.Collections.Generic;

namespace BiblioModel.DAL.Interfaces
{
    public interface ILivreRepository : IRepository<Livre>
    {
        IAsyncEnumerable<Livre> GetLivreByPage
                                (int index, int size);
        IAsyncEnumerable<Livre> GetLivreWithAuteurs
                                (Func<Livre, bool> predicate);
        IAsyncEnumerable<Livre> GetTopLivres(int count);
    }

}
