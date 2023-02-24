using Application.Contract.Persistence.Interface;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {

        public AuthorRepository(DatabaseContext context):base(context)
        {

        }
        public async Task<Author> GetAuthorByEmail(string email)
        {
            var author =await  _dbContext.Authors
                                                 .Where(x => x.AuthorEmail == email)
                                                 .FirstOrDefaultAsync();
            return author;
        }

        /*public async Task<Author> UpdateAuthor(Author author)
        {

        }*/
    }
}
