using CitiesCalculations.Model;

namespace CitiesCalculations.Helpers.DataParser
{
    internal class TxtCitiesDataParser : IDataParser<City>
    {
        public string Data { get; private set; }

        public TxtCitiesDataParser(string data)
        {
            Data = data;
        }

        public List<City> ParseData()
        {
            var cities = new List<City>();
            var lines = Data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                if (parts.Length == 4)
                {
                    var name = parts[0].Trim();
                    var x = double.Parse(parts[1].Trim());
                    var y = double.Parse(parts[2].Trim());
                    var airQuality = double.Parse(parts[3].Trim());
                    cities.Add(new City(name, x, y, airQuality));
                }
            }
            return cities;
        }
    }    
}
