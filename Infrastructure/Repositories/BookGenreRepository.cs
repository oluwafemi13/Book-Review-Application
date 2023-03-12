using Application.Contract.Persistence.Interface;
using Application.Model;
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
    internal class BookGenreRepository : IBookGenreRepository
    {
        private readonly DatabaseContext _Db;

        public BookGenreRepository(DatabaseContext db)
        {
            _Db = db;
        }

        public async Task<bool> CreateBookGenre(BookGenre entity)
        {
            var find = _Db.BookGenres
                .Where(e => e.GenreId == entity.GenreId)
                .Where(f => f.BookId == entity.BookId)
                .FirstOrDefaultAsync();
            if(find == null)
            {
                await _Db.BookGenres.AddAsync(entity);
                return true;
            }
            return false;
        }
    }
}
