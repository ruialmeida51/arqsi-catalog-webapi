using Microsoft.EntityFrameworkCore;
using Server.DTO;
using Server.Model;
using Server.Model.Restriction;
using Server.Repository.Base.DBBuilder;
using Server.Utils;

namespace Server.Repository.Base
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<AestheticLine> AestheticLines { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Finish> Finishes { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAggregation> ProductsAggregations { get; set; }
        public DbSet<ProductMaterialFinish> ProductMaterialFinishes { get; set; }
        public DbSet<ProductCatalog> ProductCatalogs { get; set; }
        public DbSet<ProductCollection> ProductCollections { get; set; }
        public DbSet<UserToDbDto> Users { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<FinishRestriction> FinishRestriction { get; set; }
        public DbSet<MaterialRestriction> MaterialRestriction { get; set; }
        public DbSet<CategoryAggregation> CategoryAggregation { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DbMaterialBuilder.Build(builder);
            DbFinishBuilder.Build(builder);
            DbProductBuilder.Build(builder);
            DbRestrictionsBuilder.Build(builder);
            DbCategoryBuilder.Build(builder);
        }
    }
}