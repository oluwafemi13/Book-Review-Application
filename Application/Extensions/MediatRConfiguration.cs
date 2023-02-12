using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
