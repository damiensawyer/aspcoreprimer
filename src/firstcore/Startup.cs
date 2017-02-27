using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Autofac;
using System;
using Autofac.Extensions.DependencyInjection;

namespace firstcore
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
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
        // public void ConfigureServices(IServiceCollection services)
        // {
        //     services.AddMvc(); //http://stackoverflow.com/a/40097363/494635
        //     services.AddSingleton(Configuration);
        //     services.AddSingleton<IGreeter, Greeter>();
        //     services.AddLogging();

        // }
        
        ///This replaces the default container with Autofac
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            services.AddSingleton(Configuration);
            services.AddMvc(); //http://stackoverflow.com/a/40097363/494635
            services.AddLogging();
            //services.AddSingleton<IGreeter, Greeter>();
        
            builder.Populate(services);
            builder.RegisterType<Greeter>().AsImplementedInterfaces();
            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
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
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("somessthing bad happened")
                });
            }


            //app.UseDefaultFiles(); // sets the file to the default... eg index.html
            //app.UseStaticFiles(); // renders file. YOU NEED BOTH
            // OR.. .just go 
            app.UseFileServer(); // this is default + static
            app.UseMvcWithDefaultRoute();

            // app.UseWelcomePage("/welcome");

            // app.Run(async (context) =>
            // {   
            //     // throw new InvalidOperationException("bad");
            //     foreach (var g in greeters)
            //     {
            //         var m = g.GetGreeting();
            //         await context.Response.WriteAsync($"{m}\n");
            //     }
            // });
        }
    }
}
