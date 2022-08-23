using FamilyTree.Persistence.Context;
using FamilyTree.Persistence.Interfaces;
using Microsoft.Extensions.Options;

namespace FamilyTree.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add context DB;
            builder.Services.Configure<FamilyTreeDatabaseContext>(builder.Configuration.GetSection("FamilyTreeDatabaseContext"));
            builder.Services.AddSingleton<IFamilyTreeDatabaseContext>(serviceProvider => 
                serviceProvider.GetRequiredService<IOptions<FamilyTreeDatabaseContext>>().Value);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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