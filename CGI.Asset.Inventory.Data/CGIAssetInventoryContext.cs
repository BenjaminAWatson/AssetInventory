using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CGI.Asset.Inventory.Domain;

namespace CGI.Asset.Inventory.Data
{
    public class CGIAssetInventoryContext : DbContext
    {
        public CGIAssetInventoryContext()
        {
        }

        public CGIAssetInventoryContext(DbContextOptions<CGIAssetInventoryContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Domain.Asset> Asset { get; set; }
        public virtual DbSet<ClientSite> ClientSite { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Product> Product { get; set; }
    }
}
