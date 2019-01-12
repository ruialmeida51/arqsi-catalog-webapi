namespace Server.Model
{
    public class Dimension : DbEntity
    {
        public Measure Height { get; set; }
        public Measure Width { get; set; }
        public Measure Depth { get; set; }
        
        public Dimension(Measure height, Measure width, Measure depth)
        {
            Height = height;
            Width = width;
            Depth = depth;
        }
        
        public Dimension(){}
    }
}