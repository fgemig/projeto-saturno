using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Saturno.Domain.Bus;
using Saturno.Domain.Commands.Handlers;
using Saturno.Domain.Contracts;
using Saturno.Domain.Events;
using Saturno.Domain.Events.Handlers;
using Saturno.Domain.Repositories;
using Saturno.Infra;
using Saturno.Infra.Repositories;
using System.Text;

namespace Saturno.API
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
            services.AddDbContext<SaturnoDataContext>(options =>
                options.UseInMemoryDatabase("SaturnoDb"));

            services.AddScoped<IEventBus, EventBus>();            

            // Event Handlers
            services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserEventHandler>();

            // Domain Handlers
            services.AddScoped<UserHandler, UserHandler>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<SaturnoDataContext>();

            var key = Encoding.ASCII.GetBytes(Settings.SecretKey);

            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.SaveToken = true;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole",
                     policy => policy.RequireRole("Admin"));

                options.AddPolicy("RequireUserRole",
                     policy => policy.RequireRole("Admin", "User"));
            });

            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c => c
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
