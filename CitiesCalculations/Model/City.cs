namespace CitiesCalculations.Model
{
    internal class City
    {
        public string Name { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double AirQuality { get; private set; }
        public City(string name, double x, double y, double airQuality)
        {
            Name = name;
            X = x;
            Y = y;
            AirQuality = airQuality;
        }
        public override string ToString()
        {
            return $"{Name} ({X}, {Y})";
        }


    }
}
