using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CBS.CheckManagementManagement.Domain.Context;
using System.Reflection;
using MediatR;
using CBS.CheckManagementManagement.MediatR.Behaviors;
using FluentValidation;
using CBS.CheckManagementManagement.API.Helpers;
using CBS.CheckManagementManagement.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CBS.APICaller.Helper;

namespace CBS.CheckManagementManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("CBS.CheckManagementManagement.MediatR");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddDbContext<CheckManagementContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(assembly);

            services.AddSingleton(MapperConfig.GetMapperConfigs());
            // services.AddDependencyInjection(); // This will be created later
            services.AddScoped<UserInfoToken>();

            var jwtConfig = Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtConfig["key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig["issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtConfig["audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("ExposeResponseHeaders", builder =>
                {
                    builder.AllowAnyOrigin()
                           .WithExposedHeaders("X-Pagination")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CBS.CheckManagementManagement.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CBS.CheckManagementManagement.API v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("ExposeResponseHeaders");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JWTMiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
