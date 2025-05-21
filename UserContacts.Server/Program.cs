using FluentValidation;
using UserContacts.Bll.Dtos;
using UserContacts.Bll.Helpers;
using UserContacts.Bll.Services;
using UserContacts.Bll.Validators;
using UserContacts.Repository.Services;
using UserContacts.Server.Configurations;
using UserContacts.Server.Endpoints;
using UserContacts.Server.Middlewares;

namespace UserContacts.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.WebHost.ConfigureKestrel(options =>
            //{
            //    options.ListenAnyIP(5000); // Hamma IP-lardan 5000-portda qabul qiladi
            //});



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Configuration();
            builder.ConfigurationJwtAuth();
            builder.ConfigureJwtSettings();
            builder.ConfigureSerilog();
            builder.Services.ConfigureDependecies();
            builder.Services.AddMemoryCache();
            builder.Services.AddMemoryCache();
            var app = builder.Build();

           
            app.MapAuthEndpoints();
            app.MapUserEndpoints();
            app.MapContactEndpoints();
            app.MapRoleEndpoints();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<RequestDurationMiddleware>();
            //app.UseMiddleware<NightBlockMiddleware>();
            app.UseMiddleware<MaintenanceMiddleware>();
            app.UseMiddleware<GeoBlockMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
