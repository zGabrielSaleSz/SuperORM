using Microsoft.Extensions.Configuration;
using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface.Repository;
using SuperORM.MySql;
using SuperORM.TestsResource.Repositories;
using SuperORM.WebAPI.Domain.Services;
using SuperORM.WebAPI.Domain.Services.Implementation;
using SuperORM.WebAPI.Infrastructure.MySqlImp;
using System.Reflection;

namespace SuperORM.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(builder.Environment.ContentRootPath)
                          .AddJsonFile("appsettings.json", false)
                          .Build();

            LoadRepositories(configuration, builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void LoadRepositories(IConfigurationRoot configuration, WebApplicationBuilder builder)
        {
            string mySqlConnectionString = configuration["MySqlConnection"];
            ConnectionProvider connectionProvider = new ConnectionProvider(mySqlConnectionString);
            RepositoryRegistry repositoryRegistry = new RepositoryRegistry(connectionProvider);

            Assembly assemblyRepositories = typeof(UserRepository).Assembly;
            repositoryRegistry.UseAllRepositories(false, assemblyRepositories);

            builder.Services.AddSingleton<IRepositoryRegistry, RepositoryRegistry>(rr =>
            {
                return repositoryRegistry;
            });

            builder.Services.AddScoped<UnityOfWork>();

            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}