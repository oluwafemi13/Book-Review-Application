using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IAuthorRepository: IGenericRepository<Author>
    {
        Task<Author> GetAuthorByEmail(string email);
    }
}
