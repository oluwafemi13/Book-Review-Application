using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IGenreRepository: IGenericRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetGenreByBook(Guid BookId);
        Task<Genre> FindGenreByName(string Name);
    }
}
