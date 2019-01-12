using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Server.Utils;

namespace Server.Model
{
    public class Measure : DbEntity
    {
        // Is measure continuous
        public bool IsContinuous { get; set; }

        // Is measure discrete
        public bool IsDiscrete { get; set; }

        // Minimum value of measure
        public Numeric MinMeasure { get; set; }

        // Maximum value of measure
        public Numeric MaxMeasure { get; set; }

        // Values for discreet measure
        public ICollection<Numeric> Measures { get; set; }

        // Constructor for continued measure
        public Measure(Numeric MinMeasure, Numeric MaxMeasure)
        {
            this.MinMeasure = MinMeasure;
            this.MaxMeasure = MaxMeasure;

            Measures = null;

            IsContinuous = true;
            IsDiscrete = false;
        }

        // Constructor for discreet measure
        public Measure(ICollection<Numeric> measures)
        {
            MinMeasure = new Numeric(-1);
            MaxMeasure = new Numeric(-1);

            Measures = measures;

            IsContinuous = false;
            IsDiscrete = true;
        }
        
        private Measure(){}
    }
}