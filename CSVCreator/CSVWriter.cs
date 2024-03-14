using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVCreator
{
    
    class CSVWriter
    {
        /// <summary>
        /// Writing data to csv file
        /// </summary>
        /// <param name="dictionarylist"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool WriteDictionaryToCsv(List<Dictionary<string, string>> dictionarylist, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Name, Value");
                    foreach (var dictionary in dictionarylist)
                    {
                        foreach (var entry in dictionary)
                        {
                            writer.WriteLine($"{entry.Key},{entry.Value}");
                        }
                        writer.WriteLine("\n New Exchange");
                    }

                }
                return true;
            }
            catch(Exception e)
            {

            }
            return false;
        }
    }
}
