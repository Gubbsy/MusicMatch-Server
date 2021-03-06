using Abstraction.Repositories;
using Abstraction.Services;
using API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicMatch_Server.FIlters;
using MusicMatch_Server.Hubs;
using MusicMatch_Server.Services;
using SQLServer;
using SQLServer.Models;
using SQLServer.Repositories;

namespace MusicMatch_Server
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
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISignInRepository, SignInRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<ISuggestionsRepository, SuggestionsRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
           

            services.AddSingleton<HttpContextAccessor, HttpContextAccessor>();

            services.AddSignalR();

            services.AddIdentity<ApplicationUserDbo, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidationFilter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseOptions();

            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
