using crt_creditgw_auth_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Data
{
    public class AppUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public IConfiguration Configuration { get; }
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=CreditGatewayUsers;Integrated Security=True;");
        //}

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<BusinessProfile> BusinessProfile { get; set; }
        public DbSet<BusinessOfficer> BusinessOfficer { get; set; }
        public DbSet<BusinessCategory> BusinessCategory { get; set; }
        public DbSet<SICCode> SICCode { get; set; }
    }
}
