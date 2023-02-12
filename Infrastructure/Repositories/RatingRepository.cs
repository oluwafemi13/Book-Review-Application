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
    public class RatingRepository: GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(DatabaseContext db) : base(db)
        {

        }
    }
}
