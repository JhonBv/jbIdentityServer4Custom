using crt_creditgw_auth_api.Creditgateway.clients;
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
        public IConfiguration _config { get; }
        public Startup(IWebHostEnvironment environment, IConfiguration Configuration)
        {
            Environment = environment;
            _config = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {            

            services.AddControllersWithViews(); //JB 27/02/2020, 14:44am

            services.AddControllers();//JB 27/02/2020, 13:52

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;            
            //JB. Configure CreditGatewayUsers DB Context
            services.AddDbContext<AppUserDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("UsersDatabase")));
            //JB. Configure ResourceDb
            services.AddDbContext<ResourceConfigDbContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("ResourceDatabase")));

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
            services.AddScoped<IScopeFactory, ScopeFactory>(x=> new ScopeFactory(x.GetRequiredService<ResourceConfigDbContext>()));
            services.AddScoped<IScopeRepository, ScopeRepository>(x => new ScopeRepository(x.GetRequiredService<ResourceConfigDbContext>()));
            services.AddScoped<ISecretsRepository, SecretsRepository>(x => new SecretsRepository(x.GetRequiredService<ResourceConfigDbContext>()));
            services.AddScoped<ISecretsFactory, SecretsFactory>();
            services.AddScoped<ISecretsService, SecretsService>();
            services.AddScoped<IResourceFactory, ResourceFactory>();
            services.AddScoped<IResourceRepository, ResourceRepository>(x => new ResourceRepository(x.GetRequiredService<ResourceConfigDbContext>()));
            services.AddScoped<IClientFactory, ClientFactory>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClaimFactory, ClaimFactory>();
            services.AddScoped<IClaimRepository, ClaimRepository>(x => new ClaimRepository(x.GetRequiredService<ResourceConfigDbContext>()));
            services.AddScoped<IGrantTypeRepository, GrantTypeRepository>(x => new GrantTypeRepository(x.GetRequiredService<ResourceConfigDbContext>()));

            //JB. Configure IdentityServer4 and Database Contexts
            var builder = services.AddIdentityServer()//options=> { options.PublicOrigin = "https://jb-crt.azurewebsites.net"; })

                //JB. Configure CreditGatewayResourceDB Configuration Context
                .AddConfigurationStore(options =>
                    {
                        //var ResourceConn = _config[];
                        options.ConfigureDbContext = b => b.UseSqlServer(_config.GetConnectionString("ResourceDatabase"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));

                    })


                //JB. Configure CreditGatewayResourceDB Persistance Context
                .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(_config.GetConnectionString("UsersDatabase"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                    }).AddAspNetIdentity<ApplicationUser>();

           
            // JB.not recommended for production - you need to store your key material somewhere secure. A certificate will be used instead.
            builder.AddDeveloperSigningCredential();
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
