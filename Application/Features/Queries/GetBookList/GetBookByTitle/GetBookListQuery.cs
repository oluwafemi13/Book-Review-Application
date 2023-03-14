using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetBookByTitle

{
    public class GetBookListQuery : IRequest<List<BookVM>>
    {
        [Required]
        public string Title { get; set; }

        public GetBookListQuery(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
    }
}
