using CrudforMedicshop.Application.Interfaces;
using CrudforMedicshop.Application.mapping;
using CrudforMedicshop.infrastructure.Services;
using Libraray.Application.Services;
using Libraray.Infrastructure.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Persistance;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace LibraryLoginHome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Add services to the container.
          
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ITokenSrvice, TokenService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.addmapping();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddCors();
            builder.Services.AddScoped<IRepository<User>, Repository>();
            builder.Services.AddScoped<Iservice<User>, Service>();
            builder.Services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(config =>
                {
                    config.SaveToken = true;
                    config.TokenValidationParameters = new()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
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