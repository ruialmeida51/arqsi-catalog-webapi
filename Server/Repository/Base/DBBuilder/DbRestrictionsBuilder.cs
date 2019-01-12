using Microsoft.EntityFrameworkCore;
using Server.Model.Restriction;

namespace Server.Repository.Base.DBBuilder
{
    public class DbRestrictionsBuilder
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<FinishRestriction>().HasKey(fR => new {fR.ProductId, fR.InvalidFinishId});
            builder.Entity<MaterialRestriction>().HasKey(mR => new {mR.ProductId, mR.InvalidMaterialId});
        }
    }
}