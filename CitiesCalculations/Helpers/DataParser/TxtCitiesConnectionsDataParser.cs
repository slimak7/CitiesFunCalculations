using CitiesCalculations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    connections.Add(new CitiesConnection(city1, city2, distance));
                }
            }
            return connections;
        }
    }
    
    
}
