using System.Text;

namespace CitiesCalculations.Model
{
    internal class CitiesConnection
    {
        public string CityName { get; private set; }
        public List<(CitiesConnection connection, double distance)> Connections { get; private set; }

        public CitiesConnection(string cityName)
        {
            CityName = cityName;
            Connections = [];
        }

        public void AddConnection(CitiesConnection connection, double distance)
        {
            Connections.Add((connection, distance));
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(CityName);
            foreach (var connection in Connections)
            {
                stringBuilder.AppendLine($"--{connection.connection.CityName}: {connection.distance} km");
            }
            return stringBuilder.ToString();
        }

    }
}
