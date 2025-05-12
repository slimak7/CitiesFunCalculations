using CitiesCalculations.Model;


namespace CitiesCalculations.Helpers.DataParser
{
    internal class TxtCitiesConnectionsDataParser : IDataParser<CitiesConnection>
    {
        public string Data { get; private set; }
        public TxtCitiesConnectionsDataParser(string data)
        {
            Data = data;
        }
        public List<CitiesConnection> ParseData()
        {
            var connections = new List<CitiesConnection>();
            var lines = Data.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                if (parts.Length == 3)
                {
                    var city1 = parts[0].Trim();
                    var city2 = parts[1].Trim();
                    var distance = double.Parse(parts[2].Trim().Replace('.', ','));

                    var conn1 = connections.FirstOrDefault(c => c.CityName == city1);
                    if (conn1 == null)
                    {
                        conn1 = new CitiesConnection(city1);
                        connections.Add(conn1);
                    }

                    var conn2 = connections.FirstOrDefault(c => c.CityName == city2);
                    if (conn2 == null)
                    {
                        conn2 = new CitiesConnection(city2);
                        connections.Add(conn2);
                    }

                    conn1.AddConnection(conn2, distance);
                    conn2.AddConnection(conn1, distance);

                }
            }
            return connections;
        }
    }
    
    
}
