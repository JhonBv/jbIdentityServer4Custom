// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using crt_creditgw_auth_api.Creditgateway.clients;
using crt_creditgw_auth_api.Creditgateway.resources;
using crt_creditgw_auth_api.Creditgateway.scope;
using crt_creditgw_auth_api.Creditgateway.services;
using crt_creditgw_auth_api.Creditgateway.services.secrets;
using crt_creditgw_auth_api.Data;
using crt_creditgw_auth_api.Models;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
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
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews(); //JB 27/02/2020, 14:44am

            services.AddControllers();//JB 27/02/2020, 13:52

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;            
            const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CreditGatewayResourceDb;Integrated Security=True;";
            //const string TestconnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CreditGatewayResourceDbTEST;Integrated Security=True;";
            const string connusers = @"Data Source=.\SQLEXPRESS;Initial Catalog=CreditGatewayUsers;Integrated Security=True;";

            //JB. Configure CreditGatewayUsers DB Context
            services.AddDbContext<AppUserDbContext>(options =>
                options.UseSqlServer(connusers));

            //services.AddDbContext<ResourceConfigDbContext>(options => options.UseSqlServer(TestconnectionString));

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

            services.AddScoped<IScopeFactory, ScopeFactory>();
            services.AddScoped<IScopeRepository, ScopeRepository>();
            services.AddScoped<ISecretsRepository, SecretsRepository>();
            services.AddScoped<ISecretsFactory, SecretsFactory>();
            services.AddScoped<ISecretsService, SecretsService>();
            services.AddScoped<IResourceFactory, ResourceFactory>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IClientFactory, ClientFactory>();
            services.AddScoped<IClientRepository, ClientRepository>();

            //JB. Configure IdentityServer4 and Database Contexts
            var builder = services.AddIdentityServer()

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
            //builder.AddResourceStore<IResourceStore>();
        }

        public void Configure(IApplicationBuilder app)
        {
            InitializeDatabase(app);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // uncomment if you want to add MVC
            app.UseStaticFiles();//JB 27/02/2020, 11:34am
            app.UseRouting();//JB 27/02/2020, 11:34am

            app.UseIdentityServer();

            // uncomment, if you want to add MVC

            //JB 27/02/2020, 11:34am (All below)
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseStaticFiles();

            //app.UseRouting();
            //app.UseIdentityServer();
            //app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //});
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                //JB. Try to make the AspNetIdentity to work and it did :D
                var context1 = serviceScope.ServiceProvider.GetRequiredService<AppUserDbContext>();
                context.Database.Migrate();
                //JB. Try to make the AspNetIdentity to work and it did :D
                context1.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.Apis)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context1.Users.Any())
                {
                    foreach (var r in Config.Users)
                    {
                        context1.Users.Add(r);
                    }
                    context1.SaveChanges();
                }
            }
        }
    }
}
