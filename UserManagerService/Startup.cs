using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using UserManagerService.Helpers;
using UserManagerService.Models;
using UserManagerService.Repositories;
using UserManagerService.Repositories.Interfaces;
using UserManagerService.Services;
using UserManagerService.Services.Interfaces;

namespace UserManagerService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserManagerServiceContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("UMSDatabase")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User Manager Service V1",
                });
            });

            //Repository Contracts
            services.AddScoped<IUserRepository, UserRepository>();

            //Services Contracts
            services.AddScoped<IUserService, UserService>();

            //Mapper profile
            services.AddAutoMapper(cfg =>
             {
                 cfg.AddProfiles(new AutoMapper.Profile[] {
                        new MappingProfile()
                 });
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
        }
    }
}
