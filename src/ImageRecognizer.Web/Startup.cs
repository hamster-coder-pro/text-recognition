using System;
using System.IO;
using System.Reflection;
using ImageRecognizer.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ImageRecognizer.Web
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
            services.AddControllers().AddNewtonsoftJson();

            services.AddHttpClient(
                HttpClients.CognitiveServices
                , configure =>
                {
                    configure.BaseAddress = Configuration.GetSection("cognitiveServices").GetValue<Uri>("serverUrl");
                    configure.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Configuration.GetSection("cognitiveServices").GetValue<string>("subscriptionKey"));
                }
            );

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1"
                        , new OpenApiInfo
                        {
                            Title = "Image Recognition Services Demo"
                            , Version = "v1"
                        }
                    );

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                }
            );
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddScoped<IRecognizeTextService, RecognizeTextService>();
            services.AddScoped<ICognitiveServicesApiClient, CognitiveServicesApiClient>();
            services.AddScoped<IRecognitionReporter, RecognitionReporter>();
            services.AddSingleton<IImageUtilsProvider, ImageUtilsProvider>();
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "";
                    //if (hostEnvironment.IsLocal() == false)
                    //{
                    //    options.RoutePrefix = "api";
                    //}
                    // string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                    options.SwaggerEndpoint($"/swagger/v1/swagger.json", "Image Recognition Services Demo v.1");

                    //foreach (var description in options.api provider.ApiVersionDescriptions)
                    //{
                    //    options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    //}
                }
            );
        }
    }
}