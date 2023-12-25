using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public interface ISerialize
    {
        public T? DeserializeCsvToObj<T>(string csv) where T : class, new();
        public string SerializeObjToCsv(object obj);
    }
}
