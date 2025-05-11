using CitiesCalculations.Helpers.DataParser;
using CitiesCalculations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesCalculations.Repos
{
    internal class CitiesConnectionsRepo : BaseRepo<CitiesConnection>
    {
        public CitiesConnectionsRepo(IDataParser<CitiesConnection> dataParser) : base(dataParser)
        {
        }
    }
}
