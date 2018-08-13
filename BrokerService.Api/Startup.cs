using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BrokerService.Libs.Scheduler;
using BrokerService.Libs.DataFetcher;
using Microsoft.EntityFrameworkCore;
using BrokerService.Database.Models;
using BrokerService.Database.Operations.Read;

namespace BrokerService.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Database config
            var connection = Configuration.GetSection("ConnectionStrings")["DefaultDb"];
            services.AddEntityFrameworkNpgsql().AddDbContext<Broker_Data_ServiceContext>(options => options.UseNpgsql(connection));

            //Database CRUD
            services.AddTransient<IReadOps, ReadOps>();

            //Background long-running task for fetching price data
            services.AddTransient<IPriceDataFetcher, PriceDataFetcher>();
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, DailyTask>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            
        }
    }
}
