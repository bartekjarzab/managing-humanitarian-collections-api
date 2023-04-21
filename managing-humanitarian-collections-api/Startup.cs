using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Models.UserModels;
using managing_humanitarian_collections_api.Middleware;
using managing_humanitarian_collections_api.Models.Products;
using managing_humanitarian_collections_api.Common;
using managing_humanitarian_collections_api.Models.Validators;
using managing_humanitarian_collections_api.Models.Collection;

namespace managing_humanitarian_collections_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //generowanie tokenu 
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);
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
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementOrganizerHandler>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementDonatorHandler>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementCommentHandler>();
            services.AddControllers().AddFluentValidation();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddDbContext<ManagingCollectionsDbContext>();

            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ICollectionPointService, CollectionPointService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            //Serwisy rejestracyjne
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddValidatorsFromAssemblyContaining<AddProductToCategoryDtoValidator>();
            //seeder bazy danych
            services.AddScoped<CollectionSeeder>();          
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();
            //dodanie swaggera
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "managing_humanitarian_collections_api", Version = "v1" });
            });
            services.AddScoped<IProductService, ProductService>();

          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CollectionSeeder seeder)
        {
            app.UseCors(x =>
            {
                x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "managing_humanitarian_collections_api v1"));
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
