﻿using CitiesCalculations.Helpers.Calculations;
using CitiesCalculations.Helpers.DataParser;
using CitiesCalculations.Repos;
using System.Reflection;

static string LoadEmbeddedResource(string resourceName)
{
    var assembly = Assembly.GetExecutingAssembly();
    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
    {
        if (stream == null)
            throw new FileNotFoundException($"Resource '{resourceName}' not found.");

        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}
string resourceName = "CitiesCalculations.CitiesFiles.MiastaWspolrzedne2.txt";
string fileContent = LoadEmbeddedResource(resourceName);

var cityRepo = new CityRepo(new TxtDataParser(fileContent));

Console.WriteLine("------------------Zadanie 1.------------------");
CalculationsHelper.Task1(cityRepo, "Praga", "Łódź");

Console.WriteLine("------------------Zadanie 2.------------------");
CalculationsHelper.Task2(cityRepo, "Warszawa", "Szczecin", "Łódź", "Poznań");

Console.WriteLine("------------------Zadanie 3.------------------");
CalculationsHelper.Task3(cityRepo, "Łódź", "Warszawa", ["Radom", "Płock", "Piotrków Trybunalski"]);

Console.WriteLine("------------------Zadanie 4.------------------");
CalculationsHelper.Task4(cityRepo, "Szczecin", "Warszawa", ["Włocławek", "Elbląg", "Toruń", "Olsztyn", "Płock"]);

Console.WriteLine("------------------Zadanie 5.------------------");
CalculationsHelper.Task5(cityRepo, "Łódź", "Gdańsk", 5, 2000);

Console.WriteLine(cityRepo.ToString());
