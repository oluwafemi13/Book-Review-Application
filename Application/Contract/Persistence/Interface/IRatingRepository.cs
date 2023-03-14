using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IRatingRepository: IGenericRepository<Rating>
    {

        Task<Rating> FindUserByGuid(string UserId, Guid BookId);
        Task<IEnumerable<Rating>> GetRatingsByBookId(Guid BookId);
        new Task DeleteAsync(Guid BookId);
    }
}
