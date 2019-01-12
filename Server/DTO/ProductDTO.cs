namespace Server.DTO
{
    public class ProductDTO
    {
        // Product DTO name
        public string Name { get; set; }
        
        // Product DTO descrption
        public string Description { get; set; }
        
        // The catalog that the product will be displayed in
        public long? CatalogID { get; set; }
        
        // The collection that the product will be displayed in
        public long? CollectionID { get; set; }
    }
}