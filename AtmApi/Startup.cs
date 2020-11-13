using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Infrastucture.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AtmApi
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
            services.AddControllersWithViews();
            services.AddSingleton<IAtm, Atm>(serviceProvider =>
            {
                return new Atm
                (
                    "12",
                    "iddqd",
                     new Money()
                     {
                         Amount = 33 * 5 + 30 * 10 + 4 *50 ,
                         Notes = new Dictionary<PaperNote, int>
                         {
                             { PaperNote.NoteFive,33},
                             { PaperNote.NoteTen, 30 },
                             { PaperNote.NoteFifty, 4 }
                         }
                     }
                )
                {
                    Creditcards = new List<CreditCard>()
                    {
                       new CreditCard() { CardNumber = "112", Summ = 100},
                       new CreditCard() { CardNumber = "113", Summ = 1000 }
                  }
                };

            });
            services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", builder =>
                {
                    builder
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler("/error");

            app.UseCors("MyCorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });
        }
    }
}
