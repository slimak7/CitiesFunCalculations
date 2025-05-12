namespace CitiesCalculations.Helpers.DataParser
{
    internal interface IDataParser<T>
    {
        List<T> ParseData();
    }
}
