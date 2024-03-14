using DynamoJsonCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileurn = "urn:adsk.wipprod:dm.lineage:8lgGO0HNSOaQ2ZzKjbIgRw";
            string filepath = @"D:\DynamoJsonCreator\Result.csv";
            var query = new Query(Data.GetHttpClient());
            var data = Exchange.GetExchangeParametersAsync(fileurn, filepath);
            var result = CSVWriter.WriteDictionaryToCsv(data.Result, Exchange.filepath);
            Console.WriteLine("Status for write process");
            Console.WriteLine(result);
            Console.ReadKey();
            Console.ReadKey();

        }
    }
}
