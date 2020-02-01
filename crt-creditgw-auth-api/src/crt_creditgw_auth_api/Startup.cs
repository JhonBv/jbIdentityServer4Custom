using crt_creditgw_auth_api.Creditgateway.clients;
using crt_creditgw_auth_api.Creditgateway.resources;
using crt_creditgw_auth_api.Creditgateway.scope;
using crt_creditgw_auth_api.Creditgateway.services.secrets;
using crt_creditgw_auth_api.Data;
using crt_creditgw_auth_api.Models;
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
            //DI
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
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
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
