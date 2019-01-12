using Microsoft.EntityFrameworkCore;
using Server.Model;

namespace Server.Repository.Base.DBBuilder
{
    public static class DbProductBuilder
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<ProductAggregation>()
                .HasKey(pA => new {pA.RootProductId, pA.SubproductId});

            builder.Entity<ProductMaterialFinish>()
                .HasKey(pMF => new {pMF.ProductId, pMF.MaterialId, pMF.FinishId});

            builder.Entity<ProductCatalog>()
                .HasKey(pMF => new {pMF.ProductId, pMF.CatalogId});

            builder.Entity<ProductCollection>()
                .HasKey(pMF => new {pMF.ProductId, pMF.CollectionId});
            
            builder.Entity<Product>().OwnsOne(p => p.Dimension, dim =>
            {
                dim.OwnsOne(h => h.Height, height =>
                {
                    height.OwnsOne(c => c.MinMeasure);
                    height.OwnsOne(c => c.MaxMeasure);
                    height.OwnsMany(c => c.Measures, ms =>
                    {
                        ms.HasForeignKey("Id");
                        ms.Property<int>("DiscreteId").ValueGeneratedOnAdd();
                        ms.HasKey("Id", "DiscreteId");
                    }).ToTable("Height");
                });
                dim.OwnsOne(w => w.Width, width =>
                {
                    width.OwnsOne(c => c.MinMeasure);
                    width.OwnsOne(c => c.MaxMeasure);
                    width.OwnsMany(c => c.Measures, ms =>
                    {
                        ms.HasForeignKey("Id");
                        ms.Property<int>("DiscreteId").ValueGeneratedOnAdd();
                        ms.HasKey("Id", "DiscreteId");
                    }).ToTable("Width");
                });
                dim.OwnsOne(d => d.Depth, depth =>
                {
                    depth.OwnsOne(c1 => c1.MinMeasure);
                    depth.OwnsOne(c => c.MaxMeasure);
                    depth.OwnsMany(c => c.Measures, ms =>
                    {
                        ms.HasForeignKey("Id");
                        ms.Property<int>("DiscreteId").ValueGeneratedOnAdd();
                        ms.HasKey("Id", "DiscreteId");
                    }).ToTable("Depth");
                });
            });
        }
    }
}