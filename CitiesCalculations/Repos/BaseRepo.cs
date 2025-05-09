using CitiesCalculations.Helpers.DataParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesCalculations.Repos
{
    internal abstract class BaseRepo<T>
    {
        protected List<T> values;
        protected BaseRepo(IDataParser<T> dataParser) 
        {
            values = dataParser.ParseData();
        }

        public T this[int index]
        {
            get { return values[index]; }
        }

        public T GetValueByCondition(Func<T, bool> condition)
        {
            return values.FirstOrDefault(condition);
        }

        public List<T> GetValuesByCondition(Func<T, bool> condition)
        {
            return values.FindAll(x => condition(x));
        }
    }
}
