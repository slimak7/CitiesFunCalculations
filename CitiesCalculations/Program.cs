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

fileContent = LoadEmbeddedResource("CitiesCalculations.CitiesFiles.MiastaPołączenia.txt");
var connectionsRepo = new CitiesConnectionsRepo(new TxtCitiesConnectionsDataParser(fileContent));

Console.WriteLine("Wczytane dane:");
Console.WriteLine($"Miasta:\n{cityRepo.ToString()}");
Console.WriteLine();
Console.WriteLine($"Połączenia:\n{connectionsRepo.ToString()}");
Console.WriteLine();

Console.WriteLine("------------------Zadanie 1.------------------\nOblicz odległość miasta 'Praga' od 'Łódź'\n");
CalculationsHelper.Task1(cityRepo, "Praga", "Łódź");

Console.WriteLine("------------------Zadanie 2.------------------" +
    "\n Oblicz długość drogi z Warszawy do Szczecina przez Łódź i Poznań\n");
CalculationsHelper.Task2(cityRepo, "Warszawa", "Szczecin", "Łódź", "Poznań");

Console.WriteLine("------------------Zadanie 3.------------------" +
    "\nWyznacz czy trasa z Łodzi do Warszawy jest szybsza przez Radom, Płock czy \r\nPiotrków Trybunalski\n");
CalculationsHelper.Task3(cityRepo, "Łódź", "Warszawa", ["Radom", "Płock", "Piotrków Tryb"]);

Console.WriteLine("------------------Zadanie 4.------------------" +
    "\nWyznacz najkrótsza trasę wycieczki ze Szczecina do Warszawy tak aby na trasie " +
    "\nwycieczki znalazły się miasta: Włocławek, Elbląg, Toruń, Olsztyn, Płock\n");
CalculationsHelper.Task4(cityRepo, "Szczecin", "Warszawa", ["Włocławek", "Elbląg", "Toruń", "Olsztyn", "Płock"]);

Console.WriteLine("------------------Zadanie 5.------------------" +
    "\nW pliku MiastaWspolrzedne2.txt W ostatniej kolumnie dodano jakość powietrza" +
    "\n(5,0 najlepsza jakość) wyznacz trasę z Łodzi do Gdańska przez dowolne 5 miast tak" +
    "\naby trasa nie przekraczała 2000km oraz aby jakość powietrza była najlepsza w trakcie drogi\n");
CalculationsHelper.Task5(cityRepo, "Łódź", "Gdańsk", 5, 2000);

Console.WriteLine("------------------Zadanie 6.------------------" +
    "\nZaimplementuj \r\nalgorytm, by znaleźć trasę z dowolnego miasta do innego, uwzględniając tylko te" +
    "\npołączenia, które istnieją w podanym pliku.\n");
string c1;
Console.WriteLine("Podaj nazwę miasta 1:");
c1 = Console.ReadLine();
string c2;
Console.WriteLine("Podaj nazwę miasta 2:");
c2 = Console.ReadLine();
CalculationsHelper.Task6(connectionsRepo, c1, c2);


