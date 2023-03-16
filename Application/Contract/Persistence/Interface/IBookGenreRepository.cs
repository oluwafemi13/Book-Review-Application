using Application.Model;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IBookGenreRepository: IGenericRepository<BookGenre>
    {
        Task CreateBookGenre(BookGenre entity);
    }
}
