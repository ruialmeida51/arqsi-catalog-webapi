using Microsoft.EntityFrameworkCore;
using Server.Model;

namespace Server.Repository.Base.DBBuilder
{
    public static class DbFinishBuilder
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<Finish>().OwnsOne(f => f.PricePSM, 
                mP =>
                {
                    mP.OwnsMany(p => p.PriceHistoryPast, pH =>
                    {
                        pH.HasForeignKey("Id");
                        pH.Property<long>("PriceHistoryPastId").ValueGeneratedOnAdd();
                        pH.HasKey("PriceHistoryPastId", "Id");
                    });
                    mP.OwnsMany(p => p.PriceHistoryFuture, pH =>
                    {
                        pH.HasForeignKey("Id");
                        pH.Property<long>("PriceHistoryFutureId").ValueGeneratedOnAdd();
                        pH.HasKey("PriceHistoryFutureId", "Id");
                    });
                }
            );
        }
    }
}