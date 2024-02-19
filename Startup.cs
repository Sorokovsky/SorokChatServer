using Microsoft.OpenApi.Models;
using SorokChatServer.Database.Context;
using SorokChatServer.Database.Repositories;
using SorokChatServer.Interfaces;
using SorokChatServer.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using SorokChatServer.Authorization;

namespace SorokChatServer
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
            services.AddControllers();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("YourCustomPolicy", policy =>
                policy.Requirements.Add(new AuthorizationRequerment()));
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(ConfigurateSwagger());

            services.AddHttpContextAccessor();
            services.AddSingleton<IChatContext, ChatContext>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddSingleton<ICookieService, CookieService>();
            services.AddSingleton<IPasswordEncoderService, PasswordEncoderService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IBearerService, BearerService>();
            services.AddScoped<IAuthorizationsService, AuthorizationsService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.Requirements.Add(new AuthorizationRequerment());
                });
                options.DefaultPolicy = options.GetPolicy("MyPolicy");
            });
            services.AddScoped<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, AuthorizationCustomHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandler(opt => { });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static Action<SwaggerGenOptions> ConfigurateSwagger()
        {
            return c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityDefinition("Cookie", new OpenApiSecurityScheme
                {
                    Description = "Cookie authentication",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Cookie,
                    Name = "refresh_token",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
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
                    },
                    {
                         new OpenApiSecurityScheme
                         {
                            Reference = new OpenApiReference
                         {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Cookie"
                            }
                         },
                         new string[] { }
                    }
                });
            };
        }
    }
}
