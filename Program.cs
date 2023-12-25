using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Reflection
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serialize = new Serialize();
            var stopWatch = new Stopwatch();
            var fClass = F.Get();
            int iterations = 100000;

            //сериализация csv
            stopWatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var serializeC = serialize.SerializeObjToCsv(fClass);
            }
            stopWatch.Stop();
            var serializeCsv = serialize.SerializeObjToCsv(fClass);

            Console.WriteLine($"Сериализация csv {iterations} итераций: \n{serializeCsv}");
            Console.WriteLine($"{stopWatch.ElapsedMilliseconds}, мс");
            stopWatch.Reset();
            //Console.WriteLine($"Время, потраченное на вывод строки = {stopWatch.ElapsedMilliseconds}, мс");

            //Десериализация csv
            stopWatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                F deserializeCsv = serialize.DeserializeCsvToObj<F>(serializeCsv);
            }
            stopWatch.Stop();
            Console.WriteLine();
            Console.Write($"Десериализация csv {iterations} итераций: ");
            Console.WriteLine($"{stopWatch.ElapsedMilliseconds}, мс");
            stopWatch.Reset();

            //Сериализация Json
            stopWatch.Start();
            for(int i = 0; i < iterations; i++)
            {
                var serializeJ = JsonConvert.SerializeObject(fClass);
            }
            stopWatch.Stop();
            var serializeJson = JsonConvert.SerializeObject(fClass);

            Console.WriteLine();
            Console.WriteLine($"Сериализация (Json) csv {iterations} итераций: \n{serializeJson}");
            Console.WriteLine($"{stopWatch.ElapsedMilliseconds}, мс");
            stopWatch.Reset();

            //Десериализация Json
            stopWatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var deserializeJ = JsonConvert.DeserializeObject(serializeJson);
            }
            stopWatch.Stop();
            var deserializeJson = JsonConvert.DeserializeObject(serializeJson);

            Console.WriteLine();
            Console.Write($"Десериализация (Json) csv {iterations} итераций: ");
            Console.WriteLine($"{stopWatch.ElapsedMilliseconds}, мс");
            stopWatch.Reset();
        }
    }
}
