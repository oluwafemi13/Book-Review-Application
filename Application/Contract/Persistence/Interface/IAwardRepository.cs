using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IAwardRepository: IGenericRepository<Award>
    {
         Task<IEnumerable<Award>> GetAllByIdAsync(int Id);
        Task<IEnumerable<Award>> GetByName(string name, int id);
    }
}
