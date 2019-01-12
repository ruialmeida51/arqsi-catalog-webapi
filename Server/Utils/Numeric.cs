using Server.Model;

namespace Server.Utils
{
    //[Owned]
    public class Numeric : DbEntity
    {
        public double Value { get; set; }

        public Numeric(double value)
        {
            Value = value;
        }
        
        private Numeric(){}
    }
}