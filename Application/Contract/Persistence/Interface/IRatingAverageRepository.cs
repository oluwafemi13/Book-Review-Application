using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IRatingAverageRepository: IGenericRepository<RatingAverage>
    {
        Task<IEnumerable<Rating>> GetRatingAverageByBook(string book);
        new Task DeleteAsync(Guid BookId);

    }
}
