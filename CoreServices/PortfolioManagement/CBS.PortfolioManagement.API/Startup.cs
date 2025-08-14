using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;
using CBS.PortfolioManagement.Domain;
using CBS.APICaller.Helper;
using CBS.PortfolioManagement.Helper;
using MediatR;
using CBS.PortfolioManagement.MediatR.PipeLineBehavior;
using FluentValidation;
using CBS.PortfolioManagement.API.Helpers;
using CBS.PortfolioManagement.Dto;
using CBS.APICaller.Helper.LoginModel.Authenthication;
// using Hangfire;
using CBS.ServicesDelivery.Service.ServiceDiscovery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CBS.PortfolioManagement.Repository.DatabaseLogging;
using CBS.PortfolioManagement.Common.DBConnection;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace CBS.PortfolioManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("CBS.PortfolioManagement.MediatR");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

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

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblies(Enumerable.Repeat(assembly, 1));
            services.AddHttpContextAccessor(); // Register IHttpContextAccessor
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var connectionStringLogger = Configuration.GetSection("Logging:Database:ConnectionString").Value;
            services.AddDbContext<PortfolioContext>(options =>
                options.UseSqlServer(connectionStringLogger));

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddDbLogger();
            });
            var connectionString = Configuration.GetSection("Conn:PortfolioDbConnectionString").Value;
            services.AddDbContext<PortfolioContext>(options => options.UseSqlServer(connectionString));

            // MongoDB Connection String and Database Name - Commented out for initial scaffolding
            // var mongoConnectionString = Configuration.GetSection("MongoDB").GetSection("DatabaseConnectionString").Value;
            // var mongoDatabaseName = Configuration.GetSection("MongoDB").GetSection("DatabaseName").Value;
            // services.AddSingleton<IMongoDbConnection>(sp => new MongoDbConnection(mongoConnectionString, mongoDatabaseName));

            // Hangfire - Commented out for initial scaffolding
            // services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            // services.AddHangfireServer();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<JwtSettings>(GetJwtSettings());
            services.AddSingleton<PathHelper>(new PathHelper(Configuration));

            services.AddScoped<UserInfoToken>(e => new UserInfoToken() { Id = string.Empty });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ThirdPartyPolicy", policy =>
                {
                    policy.RequireRole("ThirdPartyProviders");
                });
            });
            services.AddConsulConfig(Configuration);
            services.AddSingleton(MapperConfig.GetMapperConfigs());
            services.AddDependencyInjection();
            services.AddCors(options =>
            {
                options.AddPolicy("ExposeResponseHeaders",
                    builder =>
                    {
                        builder.WithOrigins()
                               .WithExposedHeaders("X-Pagination")
                               .AllowAnyHeader()
                               .AllowCredentials()
                               .WithMethods("POST", "PUT", "GET", "DELETE")
                               .SetIsOriginAllowed(host => true);
                    });
            });

            services.AddSignalR();
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Portfolio Management Micro Service API"
                });
                c.AddServer(new OpenApiServer
                {
                    Description = "Development server",
                    Url = "http://localhost:7114" // Changed port for new service
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            app.UseSwagger(c => { c.SerializeAsV2 = true; });
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint($"v1/swagger.json", "Portfolio Management");
                c.RoutePrefix = "swagger";
            });

            var serviceProvider = app.ApplicationServices;
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PortfolioContext>();
                context.Database.Migrate();
                // DatabaseInitializerService.Initialize(context); // Commented out for initial scaffolding
                var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                loggerFactory.AddProvider(new DatabaseLoggerProvider(context, httpContextAccessor));
            }

            app.UseSession();
            app.UseCors("ExposeResponseHeaders");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseMiddleware<JWTMiddleware>();
            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public JwtSettings GetJwtSettings()
        {
            return new JwtSettings
            {
                Key = Configuration["JwtSettings:key"],
                Audience = Configuration["JwtSettings:audience"],
                Issuer = Configuration["JwtSettings:issuer"],
                MinutesToExpiration = Convert.ToInt32(Configuration["JwtSettings:minutesToExpiration"])
            };
        }
    }
}
