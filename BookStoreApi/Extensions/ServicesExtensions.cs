﻿using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;
using Repositories.EfCore;
using Services.Abstract;
using Services.Concrete;

namespace BookStoreApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        }
        public static void AddConfigureRepositoryManager(this IServiceCollection services)
        {
           services.AddScoped<IRepositoryManager, RepositoryManager>();


        }
        public static void AddConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();


        }
    }
}
