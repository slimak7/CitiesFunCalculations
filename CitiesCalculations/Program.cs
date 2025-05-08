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

Console.WriteLine(cityRepo.ToString());
