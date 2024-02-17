using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SorokChatServer.Database.Context;
using SorokChatServer.Database.Repositories;
using SorokChatServer.Interfaces;
using SorokChatServer.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication;

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
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services
                .AddAuthentication(ConfigurateAuthentication())
                .AddJwtBearer(ConfigurateBearerAuthorization());
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
                    }
                });
            };
        }
        private static Action<AuthenticationOptions> ConfigurateAuthentication()
        {
            return options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            };
        }
        private Action<JwtBearerOptions> ConfigurateBearerAuthorization()
        {
            return options =>
            {
                string? secretKey = Configuration["Jwt:SecretKey"];
                ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audit"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                };
            };
        }
    }
}
