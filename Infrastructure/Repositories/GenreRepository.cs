using Application.Contract.Persistence.Interface;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<int>> GetGenreByBook(Guid BookId)
        {
            var result = await _dbContext.BookGenres.Where(x=> x.BookId== BookId).Select(x => x.GenreId).ToListAsync();
            return result;
        }

        public async Task<string> GetById(int id)
        {
            var result = _dbContext.Genres.Where(x => x.GenreId == id).Select(y => y.GenreName).ToString();
            return result;
        }
        public async Task<Genre> FindGenreByName(string Name)
        {
            var result = await _dbContext.Genres.Where(x => x.GenreName == Name).FirstOrDefaultAsync();
            return result;
        }
    }
}
