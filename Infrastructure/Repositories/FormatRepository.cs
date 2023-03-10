using Application.Contract.Persistence.Interface;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FormatRepository: GenericRepository<Format>, IFormatRepository
    {
        public FormatRepository(DatabaseContext db): base(db)
        {

        }

        public async Task<Format> FindFormat(Guid id)
        {
            var result = _dbContext.Formats.Where(x=> x.BookId== id).FirstOrDefault();
            return result ?? throw new Exception("not found");
        }
    }
}
