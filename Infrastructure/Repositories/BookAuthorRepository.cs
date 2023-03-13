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
    public class BookAuthorRepository: IBookAuthorRepository
    {
        private readonly DatabaseContext _Db;

        public BookAuthorRepository(DatabaseContext db)
        {
            _Db = db;
        }

        public async Task CreateBookAuthor(BookAuthor entity)
        {
            await _Db.BookAuthors.AddAsync(entity);
            await _Db.SaveChangesAsync();

        }
    }
}
