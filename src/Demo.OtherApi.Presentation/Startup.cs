using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Demo.OtherApi.CrossCutting.IoC;
using UtilOptions = Demo.OtherApi.Util.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;

namespace Demo.OtherApi.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration cnfs, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.AddInMemoryCollection(cnfs.AsEnumerable()).Build();
            Environment = env;
        }

        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<UtilOptions.AWSSettingOptions>(Configuration.GetSection("AWSSetting"));
            services.Configure<UtilOptions.SwaggerOptions>(Configuration.GetSection("Swagger"));

            var sp = services.BuildServiceProvider();
            var swaggerOptions = sp.GetService<IOptions<UtilOptions.SwaggerOptions>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerOptions.Value.Version, new Info
                {
                    Version = swaggerOptions.Value.Version,
                    Title = swaggerOptions.Value.Title,
                    Description = swaggerOptions.Value.Description,
                    TermsOfService = swaggerOptions.Value.TermsOfService,
                    Contact = new Contact { Name = swaggerOptions.Value.ContactName, Email = swaggerOptions.Value.ContactEmail }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Service.xml");

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }

                c.DescribeAllEnumsAsStrings();
            });

            DependencyInjectorBootStrapper.RegisterServices(services, Environment, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<UtilOptions.SwaggerOptions> swaggerOptions, IConfiguration configuration)
        {
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = swaggerOptions.Value.Title;
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint($"{configuration["Stage"]}{swaggerOptions.Value.Endpoint}", swaggerOptions.Value.Title);
            });

            app.UseMvc();
        }
    }
}
