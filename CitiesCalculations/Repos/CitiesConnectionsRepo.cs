using CitiesCalculations.Helpers.DataParser;
using CitiesCalculations.Model;

namespace CitiesCalculations.Repos
{
    internal class CitiesConnectionsRepo : BaseRepo<CitiesConnection>
    {
        public CitiesConnectionsRepo(IDataParser<CitiesConnection> dataParser) : base(dataParser)
        {
        }
    }
}
