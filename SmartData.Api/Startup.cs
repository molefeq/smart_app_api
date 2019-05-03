using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartData.Api.Extensions;
using SmartData.Api.IocContainers;
using SmartData.Api.Providers;
using SmartData.DataAccess;
using SmartData.Payfast.Models;
using SmartData.UCloudLinkApiClient;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace SmartData.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["token:issuer"],
                    ValidAudience = Configuration["token:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["token:key"])),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
            });

            services.AddAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Smart App API V1",
                    Version = "v1",
                    Description = "Smart App Web API written in ASP.NET Core Web API",
                    Contact = new Contact { Name = "Qinisela Molefe", Email = "molefeq@gmail.com", Url = "https://www.codeassembly.co.za" },
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("all_origins", policy => policy.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        );
            });
            services.AddMemoryCache();
            services.AddHttpClient<UCloudLinkClient>();
            services.AddDbContext<SmartAppContext>(options => options.UseNpgsql(Configuration.GetConnectionString("SmartApp_DB_Local")), ServiceLifetime.Transient);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.Configure<PayFastSettings>(Configuration.GetSection("PayFastSettings"));

            DataMappers.Initialise(services);
            BusinessRules.Initialise(services);
            Services.Initialise(services);
            UCloudLinkClients.Initialise(services);
            UCloudLinkServices.Initialise(services);
            PayFast.Initialise(services);

            services.AddScoped<LoginProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            bool isLocalUat = env.IsEnvironment("LOCAL_UAT");
            string swaggerJsonUrlPrefix = isLocalUat ? "/smartapp" : "";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSmartAppExceptionHandler();
            app.UseSwagger();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseBusinessPartnerLoginHandler();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{swaggerJsonUrlPrefix}/swagger/v1/swagger.json", "Smart App Web API V1");
            });
            app.UseCors("all_origins");
            app.UseMvc();
        }
    }
}
