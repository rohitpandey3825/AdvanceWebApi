using Microsoft.EntityFrameworkCore;
using ProductCommon.Interface;
using ProductsDataAccess;
using ProductsService;

namespace AdvancedWebAPIpractice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ShopContext>(options =>
            {
                options.UseInMemoryDatabase("Shop");
            });

            builder.Services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Redirect root to the Swagger UI page, but exclude this minimal endpoint from Swagger
                app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
                   .ExcludeFromDescription();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
