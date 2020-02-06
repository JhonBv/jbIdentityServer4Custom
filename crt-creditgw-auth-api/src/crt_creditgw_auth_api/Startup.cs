﻿using crt_creditgw_auth_api.Creditgateway.clients;
using crt_creditgw_auth_api.Creditgateway.resources;
using crt_creditgw_auth_api.Creditgateway.scope;
using crt_creditgw_auth_api.Creditgateway.services.claims;
using crt_creditgw_auth_api.Creditgateway.services.grants;
using crt_creditgw_auth_api.Creditgateway.services.secrets;
using crt_creditgw_auth_api.Data;
using crt_creditgw_auth_api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Reflection;

namespace crt_creditgw_auth_api
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
            Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            //JB. Overcome HTTPS Issue. Added 5/2/2020
            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor;
            //    options.RequireHeaderSymmetry = false;
            //    options.KnownNetworks.Clear();
            //    options.KnownProxies.Clear();

            //});

            

            services.AddControllersWithViews(); //JB 27/02/2020, 14:44am

            services.AddControllers();//JB 27/02/2020, 13:52

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;            
            const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CreditGatewayResourceDb;Integrated Security=True;";
            const string connusers = @"Data Source=.\SQLEXPRESS;Initial Catalog=CreditGatewayUsers;Integrated Security=True;";

            
            //JB. Configure CreditGatewayUsers DB Context
            services.AddDbContext<AppUserDbContext>(options =>
                options.UseSqlServer(connusers));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppUserDbContext>()
                .AddDefaultTokenProviders();

            //JB. Lets enable Google logins
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });

            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry();

            //JB. DI mapping
            services.AddScoped<IScopeFactory, ScopeFactory>();
            services.AddScoped<IScopeRepository, ScopeRepository>();
            services.AddScoped<ISecretsRepository, SecretsRepository>();
            services.AddScoped<ISecretsFactory, SecretsFactory>();
            services.AddScoped<ISecretsService, SecretsService>();
            services.AddScoped<IResourceFactory, ResourceFactory>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IClientFactory, ClientFactory>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClaimFactory, ClaimFactory>();
            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IGrantTypeRepository, GrantTypeRepository>();

            //JB. Configure IdentityServer4 and Database Contexts
            var builder = services.AddIdentityServer(options=> { options.PublicOrigin = "https://jb-crt.azurewebsites.net"; })

                //JB. Configure CreditGatewayResourceDB Configuration Context
                .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));

                    })


                
                //JB. Configure CreditGatewayResourceDB Persistance Context
                .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                    }).AddAspNetIdentity<ApplicationUser>();

           
            // JB.not recommended for production - you need to store your key material somewhere secure. A certificate will be used instead.
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();
            //forwardOptions.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:111.12.0.0"), 16));

            app.UseForwardedHeaders(forwardOptions);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedProto, RequireHeaderSymmetry=false});//JB. Overcome HTTPS Issue. Added 5/2/2020


            app.UseStaticFiles();//JB 27/02/2020, 11:34am
            app.UseRouting();//JB 27/02/2020, 11:34am

            app.UseIdentityServer();

            //JB 27/02/2020, 11:34am (All below)
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }   
    }
}
