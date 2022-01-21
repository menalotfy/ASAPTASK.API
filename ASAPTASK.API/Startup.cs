
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ASAPTASK.Core.Interfaces.AreaInterface;

using ASAPTASK.Core.Interfaces;

using ASAPTASK.Infrastructure.Data;
using ASAPTASK.Infrastructure.Repositories.AreaRepositories;

using Newtonsoft.Json;


using Microsoft.Extensions.Options;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ASAPTASK.Core.DTOs.AutoMapperConfigs;
using ASAPTASK.Core.Interfaces.MainInterface;
using ASAPTASK.Infrastructure.Repositories.MainRepositories;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;

namespace ASAPTASK.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // 

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.AddDbContext<ASAPContext>(x =>
            x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), f =>
            {

            }));

            
           

            var list = new List<string>();
            Configuration.GetSection("AllowedHosts").Bind(list);

            services.AddCors(options => {
                options.AddPolicy(MyAllowSpecificOrigins, builder => {
                    builder.WithOrigins(list.ToArray()).SetIsOriginAllowed(x => _ = true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

         

            services.AddAutoMapper(typeof(MainProfiler));

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    // Configure JSON to ignore reference loop
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

          



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddTransient<IDBInitializer, DBInitializer>();
            
          services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();

 


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASAPTASK.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
            });
            services.AddOptions();
            services.AddHttpClient();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDBInitializer dbInitializer)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

          


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbInitializer.Initialize();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASAPTASK.API v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
