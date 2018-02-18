using Dashboard.API.Application.Infrastructure.Identity;
using Dashboard.API.Application.Infrastructure.Persistence;
using Dashboard.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dashboard.API
{
    public class Startup
    {
        private readonly IValidateAuthToken _validateAuthToken;

        private const string JwtBearer = "JwtBearer";

        public Startup(IConfiguration configuration, IValidateAuthToken validateAuthtoken)
        {
            Configuration = configuration;
            _validateAuthToken = validateAuthtoken;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearer;
                options.DefaultChallengeScheme = JwtBearer;
            })
            .AddJwtBearer(JwtBearer, jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = _validateAuthToken.TokenValidationParameters;

                jwtBearerOptions.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        var userRole = ctx.Principal.Claims.FirstOrDefault(d => d.Type == "custom:role")?.Value;

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, userRole)
                        };

                        var appIdentity = new ClaimsIdentity(claims);

                        ctx.Principal.AddIdentity(appIdentity);

                        await Task.CompletedTask;
                    }
                };
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<SeviiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection2")));

            services.AddMvc();

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .WithOrigins("http://localhost:3000")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
