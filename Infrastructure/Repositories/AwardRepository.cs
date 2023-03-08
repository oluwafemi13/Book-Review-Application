using Application.Contract.Persistence.Interface;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AwardRepository: GenericRepository<Award>, IAwardRepository
    {
        public AwardRepository(DatabaseContext db): base(db)
        {
        }

        public async Task<IEnumerable<Award>> GetAllByIdAsync(int Id)
        {
            return await _dbContext.Awards.Where(x=> x.AuthorId == Id).ToListAsync();
        }

        public async Task<IEnumerable<Award>> GetByName(string name, int id)
        {
            return await _dbContext.Awards
                .Where(y => y.AwardTitle == name)
                .Where(x => x.AuthorId == id).ToListAsync();
        }
    }
}
