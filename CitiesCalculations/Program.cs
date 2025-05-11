using CitiesCalculations.Helpers.Calculations;
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

var cityRepo = new CityRepo(new TxtCitiesDataParser(fileContent));

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

Console.WriteLine("------------------Zadanie 6.------------------");
fileContent = LoadEmbeddedResource("CitiesCalculations.CitiesFiles.MiastaPołączenia.txt");
var connectionsRepo = new CitiesConnectionsRepo(new TxtCitiesConnectionsDataParser(fileContent));
string c1;
Console.WriteLine("Podaj nazwę miasta 1:");
c1 = Console.ReadLine();
string c2;
Console.WriteLine("Podaj nazwę miasta 2:");
c2 = Console.ReadLine();
CalculationsHelper.Task6(connectionsRepo, c1, c2);

Console.WriteLine(cityRepo.ToString());
Console.WriteLine(connectionsRepo.ToString());
