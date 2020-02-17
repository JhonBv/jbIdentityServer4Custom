

using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Data
{
    public class ResourceConfigDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public DbSet<Client> Clients { get; set; }
        /// <summary>
        /// Gets or sets the identity resources.
        /// </summary>
        /// <value>
        /// The identity resources.
        /// </value>
        public DbSet<IdentityResource> IdentityResources { get; set; }
        /// <summary>
        /// Gets or sets the API resources.
        /// </summary>
        /// <value>
        /// The API resources.
        /// </value>
        public DbSet<ApiResource> ApiResources { get; set; }

        public DbSet<ClientScope> ClientScopes { get; set; }

        public DbSet<ApiScope> ApiScopes { get; set; }

        public DbSet<ApiSecret> ApiSecrets { get; set; }

        public DbSet<ClientSecret> ClientSecrets { get; set; }

        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        public DbSet<ClientClaim> ClientClaims { get; set; }
        public DbSet<ClientProperty> ClientProperties { get; set; }
        public DbSet<ApiResourceClaim> ApiClaims { get; set; }
        public DbSet<ApiResourceProperty> ApiProperties { get; set; }
        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }
        public ResourceConfigDbContext(DbContextOptions<ResourceConfigDbContext> options)
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
    }
}
