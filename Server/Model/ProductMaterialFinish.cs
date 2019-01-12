namespace Server.Model
{
    public class ProductMaterialFinish
    {
        // Id of product
        public long ProductId { get; set; }

        // Id of material
        public long MaterialId { get; set; }

        // Id of finish
        public long FinishId { get; set; }
    }
}