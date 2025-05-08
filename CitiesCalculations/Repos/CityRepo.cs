using CitiesCalculations.Helpers.DataParser;
using CitiesCalculations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesCalculations.Repos
{
    internal class CityRepo : BaseRepo<City>
    {
        public CityRepo(IDataParser<City> dataParser) : base(dataParser)
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (City city in values)
            {
                sb.AppendLine(city.ToString());
            }
            return sb.ToString();
        }
    }
}
