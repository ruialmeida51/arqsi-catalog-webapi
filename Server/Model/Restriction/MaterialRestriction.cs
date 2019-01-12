namespace Server.Model.Restriction
{
    public class MaterialRestriction
    {
        // ============
        //  Attributes
        // ============
        
        // Id of product
        public long ProductId { get; set; }

        // Invalid material id (Not done by henrique)
        public long InvalidMaterialId { get; set; }
    }
}