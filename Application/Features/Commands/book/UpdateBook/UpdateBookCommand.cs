﻿using Application.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.book.UpdateBook
{
    public class UpdateBookCommand: IRequest<Response>
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        //public int AuthorId { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime DatePublished { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string CoverImage { get; set; }
        public string FormatType { get; set; }
        public int NumberOfPages { get; set; }
    }
}
