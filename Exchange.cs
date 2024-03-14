using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using DynamoUnits;
using Autodesk.DesignScript.Geometry;
namespace DynamoJsonCreator
{
    
    public class Exchange
     {
        public static List<Dictionary<string, string>> dictionaryList;
        public static string filepath;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileUrn" description="FileURN to fetch the data of exchange"></param>
        /// <param name="FilePathofResult" description="FilePath To Write the Retrieved Data"></param>
        /// <returns></returns>
        
        public static async Task<List<Dictionary<string, string>>> GetExchangeParametersAsync(string fileUrn, string FilePathofResult)
        {
            filepath = FilePathofResult;
            try
            {
                var data =  Task.Run(()=>Query.GetExchangePropertyData(QueryString.PropertyByURN, fileUrn, "").Result).Result;
                dictionaryList = data;
                return data;
            }
            catch(Exception e)
            {
                 
            }
            return null;
        }
       
    }
}
