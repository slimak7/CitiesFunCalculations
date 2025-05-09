using CitiesCalculations.Model;
using CitiesCalculations.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CitiesCalculations.Helpers.Calculations
{
    internal static class CalculationsHelper
    {
        public static void Task1(CityRepo cityRepo, string city1, string city2)
        {
            var c1 = cityRepo.GetValueByCondition(c => c.Name == city1);
            var c2 = cityRepo.GetValueByCondition(c => c.Name == city2);

            var distance = CalculateStraightDistance(c1, c2);

            Console.WriteLine($"Odległość między {c1.Name} a {c2.Name} wynosi {distance} km");
        }

        public static void Task2(CityRepo cityRepo, string cityStart, string cityEnd, string cityOption1, string cityOption2)
        {
            var startCity = cityRepo.GetValueByCondition(c => c.Name == cityStart);
            var endCity = cityRepo.GetValueByCondition(c => c.Name == cityEnd);
            var option1City = cityRepo.GetValueByCondition(c => c.Name == cityOption1);
            var option2City = cityRepo.GetValueByCondition(c => c.Name == cityOption2);

            var distances = GetDistances(startCity, endCity, [option1City, option2City]);

            Console.WriteLine($"Trasa z {startCity.Name} do {endCity.Name}" +
                $" wynosi: {distances[0]} km jadąc przez {option1City.Name} oraz {distances[1]} km jadąc przez {option2City.Name}");
        }

        public static void Task3(CityRepo cityRepo, string cityStart, string cityEnd, List<string> options)
        {
            var startCity = cityRepo.GetValueByCondition(c => c.Name == cityStart);
            var endCity = cityRepo.GetValueByCondition(c => c.Name == cityEnd);
            List<City> optionCities = new List<City>();
            foreach (var option in options)
            {
                var city = cityRepo.GetValueByCondition(c => c.Name == option);
                if (city != null)
                {
                    optionCities.Add(city);
                }
            }
            var distances = GetDistances(startCity, endCity, optionCities);
            var minDistance = distances.Min();
            var minIndex = distances.IndexOf(minDistance);
            Console.WriteLine($"Trasa z {startCity.Name} do {endCity.Name}" +
                $" mając do wyboru: {options.Aggregate((x1, x2) => x1 + ", " + x2)} jest najszybsza przez {options[minIndex]}" +
                $" i wynosi {distances[minIndex]} km");
        }

        public static void Task4(CityRepo cityRepo, string cityStart, string cityEnd, List<string> citiesToVisit)
        {
            var startCity = cityRepo.GetValueByCondition(c => c.Name == cityStart);
            var endCity = cityRepo.GetValueByCondition(c => c.Name == cityEnd);
            List<City> optionCities = new List<City>();
            foreach (var option in citiesToVisit)
            {
                var city = cityRepo.GetValueByCondition(c => c.Name == option);
                if (city != null)
                {
                    optionCities.Add(city);
                }
            }
            List<int> citiesOrder = new List<int>();

            City currentCity = startCity;

            while (optionCities.Count > 0)
            {
                int minDistance = int.MaxValue;
                int minIndex = -1;
                for (int i = 0; i < optionCities.Count; i++)
                {
                    var distance = CalculateStraightDistance(currentCity, optionCities[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minIndex = i;
                    }
                }
                
                currentCity = optionCities[minIndex];
                citiesOrder.Add(citiesToVisit.IndexOf(currentCity.Name));
                optionCities.RemoveAt(minIndex);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append($"Najkrótsza trasa to: {startCity.Name}--");
            foreach (var index in citiesOrder)
            {
                sb.Append($"--{citiesToVisit[index]}--");
            }
            sb.Append($"--{endCity.Name}");
            Console.WriteLine(sb.ToString());   
        }

        public static void Task5(CityRepo cityRepo, string cityStart, string cityEnd, int numberOfCitiesToVisit, int distanceLimit)
        {
            var startCity = cityRepo.GetValueByCondition(c => c.Name == cityStart);
            var endCity = cityRepo.GetValueByCondition(c => c.Name == cityEnd);

            List<City> citiesInOrder = new List<City>();
            List<City> finalCityOrder = new List<City>();
            double airQuality = double.MinValue;
            int totalDistance = 0;

            CalculateRoadWithBestAirQuality(ref citiesInOrder, ref finalCityOrder, ref airQuality, startCity, endCity, cityRepo, ref numberOfCitiesToVisit, ref totalDistance, in distanceLimit);

            StringBuilder sb = new StringBuilder();
            foreach (var city in finalCityOrder)
            {
                sb.Append($"--{city.Name}--");
            }
            Console.WriteLine($"Podróż z {startCity.Name} do {endCity.Name}" +
                $" z najlepsza jakościa powietrza { double.Round(airQuality, 2)}" +
                $" dla liczby przystanków {numberOfCitiesToVisit} i limitem {distanceLimit} km to: {sb}");
        }

        private static void CalculateRoadWithBestAirQuality(
            ref List<City> cities,
            ref List<City> finalOrder,
            ref double airQuality,
            City startCity,
            City endCity,
            CityRepo cityRepo,
            ref int numberOfCitiesToVisitBetween,
            ref int totalDistance,
            in int distanceLimit)
        {
            if (cities.Any())
            {
                if (cities.Count == numberOfCitiesToVisitBetween + 1)
                {
                    var distance = CalculateStraightDistance(cities.Last(), endCity);

                    if (totalDistance + distance <= distanceLimit)
                    {
                        totalDistance += distance;
                        cities.Add(endCity);
                        var quality = cities.Sum(c => c.AirQuality) / cities.Count;
                        if (quality > airQuality)
                        {
                            airQuality = quality;
                            finalOrder = [.. cities]; 
                        }
                    }
                }
                else
                {
                    
                    var citiesToVisit = GetCitiesToVisit(cityRepo, cities, startCity, endCity);
                    foreach (var city in citiesToVisit)
                    {
                        var distance = CalculateStraightDistance(cities.Last(), city);
                        if (totalDistance + distance <= distanceLimit)
                        {
                            totalDistance += distance;
                            cities.Add(city);
                            CalculateRoadWithBestAirQuality(ref cities, ref finalOrder, ref airQuality, startCity, endCity, cityRepo, ref numberOfCitiesToVisitBetween, ref totalDistance, in distanceLimit);
                            totalDistance -= distance;
                            cities.RemoveAt(cities.Count - 1);
                        }
                    }
                }
            }
            else
            {
                cities.Add(startCity);
                CalculateRoadWithBestAirQuality(ref cities, ref finalOrder, ref airQuality, startCity, endCity, cityRepo, ref numberOfCitiesToVisitBetween, ref totalDistance, in distanceLimit);
            }
        }

        private static List<City> GetCitiesToVisit(CityRepo cityRepo, List<City> cities, City startCity, City endCity)
        {
            // Fix: Move the lambda expression here to avoid using 'ref' inside it
            return cityRepo.GetValuesByCondition(c => !cities.Contains(c) && c != startCity && c != endCity).ToList();
        }

        

        private static List<int> GetDistances(City startCity, City endCity, List<City> cities)
        {
            List<int> distances = new List<int>();
            foreach (var city in cities)
            {
                if (city != startCity && city != endCity)
                {
                    distances.Add(CalculateStraightDistance(startCity, city) + CalculateStraightDistance(city, endCity));
                }
            }
            return distances;
        }

        private static int CalculateStraightDistance(City city1, City city2)
        {
            return (int)(Math.Sqrt(Math.Pow(city2.X - city1.X, 2) + Math.Pow(city2.Y - city1.Y, 2)) + 0.5d);
        }
    }
    
}
