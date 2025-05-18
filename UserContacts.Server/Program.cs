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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Configuration();
            builder.ConfigurationJwtAuth();
            builder.ConfigureJwtSettings();
            builder.ConfigureSerilog();

            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IContactService, ContactService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
            builder.Services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();
            builder.Services.AddScoped<IValidator<ContactCreateDto>, ContactCreateDtoValidator>();
            builder.Services.AddScoped<IValidator<ContactDto>, ContactDtoValidator>();

            var app = builder.Build();
            app.MapAuthEndpoints();
            app.MapUserEndpoints();
            app.MapContactEndpoints();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
