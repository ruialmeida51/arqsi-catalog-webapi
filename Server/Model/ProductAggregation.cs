namespace Server.Model
{
    public class ProductAggregation
    {
        // ============
        //  Attributes
        // ============
        
        // Id of root product
        public long RootProductId { get; set; }

        // Id of sub-product 
        public long SubproductId { get; set; }

        //  If sub-product is required
        public bool IsRequired { get; set; }
    }
}