using Microsoft.EntityFrameworkCore;
using Server.Model;

namespace Server.Repository.Base.DBBuilder
{
    public class DbCategoryBuilder
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<CategoryAggregation>()
                .HasKey(cA => new {cA.RootCategoryId, cA.SubcategoryId});
        }
    }
}