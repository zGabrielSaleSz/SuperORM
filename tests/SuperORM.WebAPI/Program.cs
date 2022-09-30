using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Domain.Service.Settings;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using SuperORM.MySql;
using SuperORM.TestsResource.Repositories;
using System.Reflection;

namespace SuperORM.WebAPI
{
    public class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            Configuration = new ConfigurationBuilder()
                          .SetBasePath(builder.Environment.ContentRootPath)
                          .AddJsonFile("appsettings.json", false)
                          .Build();

            
            LoadRepositories(builder);
           

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

        private static void LoadRepositories(WebApplicationBuilder builder)
        {
            string mySqlConnectionString = Configuration["MySqlConnection"];
            ConnectionProvider connectionProvider = new ConnectionProvider(mySqlConnectionString);
            RepositoryRegistry repositoryRegistry = new RepositoryRegistry(connectionProvider);

            Assembly assemblyRepositories = typeof(UserRepository).Assembly;
            repositoryRegistry.UseAllRepositories(false, assemblyRepositories);

            builder.Services.AddSingleton<IRepositoryRegistry, RepositoryRegistry>(rr =>
            {
                return repositoryRegistry;
            });
        }
    }
}