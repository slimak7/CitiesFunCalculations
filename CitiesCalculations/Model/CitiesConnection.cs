using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesCalculations.Model
{
    internal class CitiesConnection
    {
        public string City1 { get; private set; }
        public string City2 { get; private set; }
        public double Distance { get; private set; }

        public CitiesConnection(string city1, string city2, double distance)
        {
            City1 = city1;
            City2 = city2;
            Distance = distance;
        }

        public override string ToString()
        {
            return $"{City1} - {City2}: {Distance} km";
        }
    }
}
