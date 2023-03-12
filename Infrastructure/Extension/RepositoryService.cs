using Application.Contract.Persistence.Interface;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extension
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepositoryService(this IServiceCollection service)
        {

            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IAuthorRepository, AuthorRepository>();
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddTransient<IReviewRepository, ReviewRepository>();
            service.AddTransient<IRatingRepository, RatingRepository>();
            service.AddTransient<IGenreRepository, GenreRepository>();
            service.AddTransient<IFormatRepository, FormatRepository>();
            service.AddTransient<IRatingAverageRepository, RatingAverageRepository>();
            service.AddTransient<IAwardRepository, AwardRepository>();
            service.AddTransient<IBookGenreRepository, BookGenreRepository>();




            return service;
        }
    }
}
