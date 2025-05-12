using CitiesCalculations.Helpers.DataParser;
using CitiesCalculations.Model;

namespace CitiesCalculations.Repos
{
    internal class CityRepo : BaseRepo<City>
    {
        public CityRepo(IDataParser<City> dataParser) : base(dataParser)
        {
        }
    }
}
