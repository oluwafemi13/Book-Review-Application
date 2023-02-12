using Application.Contract.Persistence.Interface;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(DatabaseContext db): base(db)
        {

        }
        public Task<IEnumerable<Genre>> GetGenreByBook(Guid BookId)
        {
            throw new NotImplementedException();
        }
    }
}
