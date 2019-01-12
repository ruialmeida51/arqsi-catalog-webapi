namespace Server.Model.Restriction
{
    public class FinishRestriction
    {
        // ============
        //  Attributes
        // ============
        
        // Id of product
        public long ProductId { get; set; }

        // Invalid finish id (Not done by henrique)
        public long InvalidFinishId { get; set; }
    }
}