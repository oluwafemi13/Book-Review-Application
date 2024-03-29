﻿using Application.Contract.Persistence.Interface;
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
    public class BookGenreRepository : GenericRepository<BookGenre>, IBookGenreRepository
    {
        private readonly DatabaseContext _Db;

        public BookGenreRepository(DatabaseContext db): base(db)
        {
            _Db = db;
        }

        public async Task CreateBookGenre(BookGenre entity)
        {
                await _Db.BookGenres.AddAsync(entity);
                await _Db.SaveChangesAsync();
               
        }

        public async Task<IEnumerable<int>> GetGenreByBook(Guid BookId)
        {
            var result = await _dbContext.BookGenres.Where(x => x.BookId == BookId).Select(x => x.GenreId).ToListAsync();
            return result;
        }
    }
}
