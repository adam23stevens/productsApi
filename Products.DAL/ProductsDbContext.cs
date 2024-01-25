using System;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Products.DAL.Entity;

namespace Products.DAL
{
    public class ProductsDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public ProductsDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Colour> Colours { get; set; }
    }
}

