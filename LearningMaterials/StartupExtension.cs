using FluentValidation;
using FluentValidation.AspNetCore;
using LearningMaterials.Data;
using LearningMaterials.Entities;
using LearningMaterials.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace LearningMaterials
{
    public static class StartupExtension
    {
        public static void AddPersistanceLayer(this IServiceCollection services, IConfiguration configuration, Startup startup)
        {

            services.AddDbContext<MaterialsDbContext>(options => options.UseSqlServer
                (configuration.GetConnectionString("MaterialsConStr")));
            services.AddScoped<MaterialsSeeder>();

            services.AddControllers().AddFluentValidation().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IMaterialTypeRepository, MaterialTypeRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        public static void AddAuthenticationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtDetails = new JwtDetails();
            configuration.GetSection("Authentication").Bind(jwtDetails);
            services.AddSingleton(jwtDetails);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtDetails.JwtIssuer,
                    ValidAudience = jwtDetails.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtDetails.JwtKey)),
                };
            });
            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
        }
        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    );
            });
        }
    }
}