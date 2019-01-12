namespace Server.Model
{
    public class CategoryAggregation
    {
        // Id of root category
        public long RootCategoryId { get; set; }

        // Id of sub-category 
        public long SubcategoryId { get; set; }
    }
}