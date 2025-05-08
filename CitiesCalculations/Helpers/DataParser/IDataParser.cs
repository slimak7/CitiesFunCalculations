using CitiesCalculations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesCalculations.Helpers.DataParser
{
    internal interface IDataParser<T>
    {
        List<T> ParseData();
    }
}
