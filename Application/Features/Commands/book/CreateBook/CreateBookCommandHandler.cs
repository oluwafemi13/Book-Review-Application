﻿using Application.Contract.Persistence.Interface;
using Application.Features.Commands.author.CreateAuthor;
using Application.Model;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.book.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, string>
    {
        private readonly IBookRepository _BookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<CreateBookCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IBookRepository BookRepository,
                                        ILogger<CreateBookCommandHandler> logger,
                                        IMapper mapper,
                                        IAuthorRepository authorRepository)
        {
            _authorRepository= authorRepository;
            _BookRepository = BookRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            //var check = await _BookRepository.GetByGuidAsync(request.BookId);
            var findBook = await _BookRepository.GetBookByISBN(request.ISBN);
            //var findAuthor = await _authorRepository.GetByNameAsync(request.Author);
            if (findBook != null)
            {
                    _logger.LogError($"Book Already Exist");
                    return findBook.ISBN.ToString();
              
            }
            string clearedIn = request.ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();

            //validate ISBN 10 digits number
            bool valid = validateISBN10(request.ISBN);
            if (valid == false)
            {
                _logger.LogInformation("Invalid ISBN Number");
                return "Invalid ISBN Number";
                
            }
               

            var book = new Book()
            {
                ISBN = clearedIn,
                BookId = Guid.NewGuid(),
                CoverImage = request.CoverImage,
                Description = request.Description,
                Language = request.Language,
                BookTitle = request.BookTitle,
                Summary = request.Summary,
                DatePublished = request.DatePublished,
                Author = request.Author  

            };
            //var map = _mapper.Map<Book>(request);
            var create = await _BookRepository.AddAsync(book);
            return create.BookId.ToString();
        }



        private bool validateISBN(string ISBN)
        {
            List<int> list = new List<int>();
            int total = 0;
            string converted = "";
            if (ISBN.Length == 10)
            {
                converted = convert10to13(ISBN);

            }
            char[] ch = converted.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                list.Add(Convert.ToInt32(ch[i]));
            }
            foreach (int i in list)
            {
                total += i;
            }
            if (total % 10 == 0)
                return true;
            return false;
        }

        private string convert10to13(string ISBN)
        {
            int count = 1;
            int weight1 = 1;
            int weight2 = 3;
            string prefix = "978";
            List<string> list = new List<string>();
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            StringBuilder builder = new StringBuilder();
            string clearedIn = ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();
            string removeLastcharacter = clearedIn.Remove(9);
            list.Add(prefix + removeLastcharacter);
            for (int i = 0; i < list.Count; i++)
            {
                list2.Add(Convert.ToInt32(list[i]));
                list3.Add(Convert.ToInt32(list[i]));
            }

            //calculation to get the last digit
            for (var j = 1; j <= list2.Count; j++)
            {
                if (j % 2 == 0)
                {
                    list2[j] = list2[j] * 3;
                }

            }

            var sum = Sum(list2);
            int checkDigit = 10 - sum % 10;
            list3.Add(checkDigit);

            foreach (var i in list3)
                builder.Append(i);

            return builder.ToString();

        }

        private bool validateISBN10(string ISBN)
        {
            string clearedIn = ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();

            //Eingabe nach int[] parsen
            int[] numbers = clearedIn.ToCharArray().Select(i => i == 'X' ? 10 : int.Parse(i.ToString())).ToArray();

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += numbers[i] * (10 - i);
            }

            if (sum % 11 == 0)
            {
                return true;
            }
            return false;
        }

        private int Sum(List<int> list)
        {
            int result = 0;
            for (var i = 0; i < list.Count; i++)
            {
                result += list[i];
            }
            return result;
        }
    }
}
