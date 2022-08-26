using FamilyTree.Persistence.Context;
using FamilyTree.Persistence.Interfaces;
using FamilyTree.Persistence.Repositories;
using FamilyTree.Service.Interfaces;
using FamilyTree.Service.Processors;
using FamilyTree.Service.Services;
using Microsoft.Extensions.Options;

namespace FamilyTree.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            // Add context DB;
            services.Configure<FamilyTreeDatabaseContext>(builder.Configuration.GetSection("FamilyTreeDatabaseContext"));
            services.AddSingleton<IFamilyTreeDatabaseContext>(
                serviceProvider => 
                serviceProvider.GetRequiredService<IOptions<FamilyTreeDatabaseContext>>().Value);

            //Add repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Add services to the container.
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ITreeProcessor, TreeProcessor>();

            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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
    }
}