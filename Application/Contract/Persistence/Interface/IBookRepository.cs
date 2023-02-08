using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IBookRepository: IGenericRepository<Book>
    {
        Task<Book> GetBookByISBN(string ISBN);
        Task<IEnumerable<Book>> GetBookByLanguage(string Language);
        Task<IEnumerable<Book>> GetBookByRatingAverage(double RatingAverage);
    }
}
