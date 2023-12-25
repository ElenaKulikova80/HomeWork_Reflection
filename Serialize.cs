using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Serialize : ISerialize
    {
        private const string Separator = ",";
        private const char charSeparator = ',';
        private static readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        public string SerializeObjToCsv(object obj)
        {

            StringBuilder stringBuilder = new StringBuilder();
            FieldInfo[] fieldInfo = obj.GetType().GetFields(bindingAttr: bindingFlags);

            foreach (FieldInfo field in fieldInfo)
            {
                string name = field.Name;
                string value = field.GetValue(obj).ToString();

                stringBuilder.Append($"'{name}'{Separator}'{value}'\n ");
            }

            return stringBuilder.ToString();
        }
        public T? DeserializeCsvToObj<T>(string csv) where T : class, new()
        {
            var instance = new T();
            string[] parts = csv.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < parts.Length; i++)
            {
                string[] fields = parts[i].Split($"{Separator}", (char)StringSplitOptions.RemoveEmptyEntries);
                if (fields.Length != 2)
                {
                    continue;
                }

                string fName = fields[0].Replace("'", "");
                string fValue = fields[1].Replace("'", "");
                FieldInfo fieldInfo = instance.GetType().GetField(name: fName, bindingAttr: bindingFlags);

                var value = Convert.ChangeType(fValue, fieldInfo.FieldType);
                fieldInfo.SetValue(obj: instance, value: value);
            }
            return instance;
        }
    }
}
