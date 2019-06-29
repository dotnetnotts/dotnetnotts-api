using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Meetup.Api;

namespace dotnetnotts_api
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
            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });
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
            app.UseCors();

            ConfigureMeetupApi();
        }

        private void ConfigureMeetupApi()
        {
            var clientId = Configuration["dotnetnotts-api:ClientId"];
            var clientSecret = Configuration["dotnetnotts-api:ClientSecret"];

            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentException("ClientId is not set");
            if (string.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentException("ClientSecret is not set");

            MeetupApi.ConfigureOauth(clientId, clientSecret);
        }
    }
}
