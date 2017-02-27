using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace firstcore
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IEnumerable<IGreeter> greeters)
        {
            loggerFactory.AddConsole(LogLevel.Trace);
            
            app.UseWelcomePage("/welcome");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }   
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions {
                    ExceptionHandler = context => context.Response.WriteAsync("somessthing bad happened")
                });
            }

            app.UseWelcomePage("/welcome");

            app.Run(async (context) =>
            {   
                // throw new InvalidOperationException("bad");
                foreach (var g in greeters)
                {
                    var m = g.GetGreeting();
                    await context.Response.WriteAsync($"{m}\n");
                }
                
                
            });
        }
    }
}
