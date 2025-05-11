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

            CalculateRoadWithBestAirQuality(citiesInOrder, finalCityOrder, ref airQuality, startCity, endCity, cityRepo, numberOfCitiesToVisit, totalDistance, distanceLimit);

            StringBuilder sb = new StringBuilder();
            foreach (var city in finalCityOrder)
            {
                sb.Append($"--{city.Name}--");
            }
            Console.WriteLine($"Podróż z {startCity.Name} do {endCity.Name}" +
                $" z najlepsza jakościa powietrza {double.Round(airQuality, 2)}" +
                $" dla liczby przystanków {numberOfCitiesToVisit} i limitem {distanceLimit} km to: {sb}");
        }

        private static void CalculateRoadWithBestAirQuality(
        List<City> currentPath,
        List<City> bestPath,
        ref double bestAirQuality,
        City start,
        City end,
        CityRepo repo,
        int maxIntermediate,
        int distanceSoFar,
        int distanceLimit)
        {
            if (currentPath.Count > maxIntermediate + 1) return;

            if (currentPath.Count == 0)
            {
                currentPath.Add(start);
            }
            else
            {
                City last = currentPath.Last();
                if (currentPath.Count == maxIntermediate + 1)
                {
                    int toEnd = CalculateStraightDistance(last, end);
                    if (distanceSoFar + toEnd <= distanceLimit)
                    {
                        var fullPath = new List<City>(currentPath) { end };
                        double avgAir = fullPath.Average(c => c.AirQuality);
                        if (avgAir > bestAirQuality)
                        {
                            bestAirQuality = avgAir;
                            bestPath.Clear();
                            bestPath.AddRange(fullPath);
                        }
                    }
                    return;
                }
            }

            foreach (var next in GetCitiesToVisit(repo, currentPath, start, end))
            {
                int toNext = CalculateStraightDistance(currentPath.Last(), next);
                if (distanceSoFar + toNext <= distanceLimit)
                {
                    currentPath.Add(next);
                    CalculateRoadWithBestAirQuality(currentPath, bestPath, ref bestAirQuality, start, end, repo, maxIntermediate, distanceSoFar + toNext, distanceLimit);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
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
