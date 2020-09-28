using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Configuration;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Aramis.TrainMovementData.WebAPI.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Data.Common;

namespace Aramis.TrainMovementData.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            DbConnectionStringBuilder connectionStringBuilder = new DbConnectionStringBuilder
            {
                ConnectionString = Configuration.GetConnectionString("Default")
            };

            services.AddControllers();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"))
                .AddDbContext<AramisDbContext>(ServiceLifetime.Transient)
                .AddSingleton(connectionStringBuilder)
                .AddTransient<IGeoDataRepository, GeoDataRepository>()
                .AddTransient<ITrainnumberRepository, TrainnumberRepository>()
                .AddTransient<IVehicleRepository, VehicleRepository>()
                .AddTransient<IBasicDataRepository, BasicDataRepository>()
                .AddTransient<INotificationRepository, NotificationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSerilogRequestLogging();
            }

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
