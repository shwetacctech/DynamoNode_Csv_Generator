using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DynamoJsonCreator
{
    [IsVisibleInDynamoLibrary(false)]
    public class Query
    {
        /// <summary>
        /// Variables required in query fetching
        /// </summary>
        const string GraphQLEndpoint = "https://developer.api.autodesk.com/dataexchange/2023-05/graphql";
        public static string AccessToken;
        public static Query queryObj = new Query();
        private static HttpClient httpClient;
        private static bool trial = false;
        public Query(){} 
        public Query(HttpClient inhttpClient)
        {
            httpClient = inhttpClient;
            trial = true;
        }
        private static HttpClient GetHttpClient()
        {
            if (!trial)
            {
                httpClient = new HttpClient();
                return httpClient;
            }
            else
            {
                return httpClient;
            }
        }
        /// <summary>
        /// Fetches the data from GraphQL 
        /// </summary>
        /// <param name="PropertyByURN"></param>
        /// <param name="fileUrn"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public static async Task<List<Dictionary<string, string>>> GetExchangePropertyData(string PropertyByURN, string fileUrn, string cursor)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            using (httpClient = GetHttpClient())
            {
                AccessToken = Authentication.GetToken();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AccessToken}");
                var variables = GetVariables(fileUrn, cursor);
                var requestData = new { PropertyByURN, variables };
                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(GraphQLEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(responseContent);
                    JArray resultsArray = (JArray)jsonObject["data"]["exchangeByFileId"]["elements"]["results"];
                    foreach (JObject res in resultsArray)
                    {
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        var serialializeResponse1 = res["properties"];
                        var serialializeResponse2 = serialializeResponse1["results"];
                        foreach (var property in serialializeResponse2)
                        {
                            var name = property["name"].ToString();
                            var value = property["value"].ToString();
                            
                            dict.Add(name, value);
                        }
                        result.Add(dict);
                    }

                    string newCursor = "";
                    try
                    {
                        newCursor = jsonObject["data"]["exchangeByFileId"]["elements"]["pagination"]["cursor"].ToString();
                        if (newCursor != "" && newCursor != null)
                        {
                            result.AddRange(await GetExchangePropertyData(PropertyByURN, fileUrn, newCursor));
                        }

                    }
                    catch (Exception e)
                    {
                        newCursor = null;
                    }
                    
                    return result;
                }
                return result;
            }
        }
        /// <summary>
        /// Generates the variables to put into query
        /// </summary>
        /// <param name="exchangeFileId"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public static object GetVariables(string exchangeFileId, string cursor = "")
        {
            object variables = null;
            object pagination = null;

            if (cursor != null && cursor != "")
            {
                int limit = 200;
                pagination = new { limit, cursor };
                variables = new { exchangeFileId, pagination };
            }
            else
            {
                int limit = 200;
                pagination = new { limit };
                variables = new { exchangeFileId, pagination };
            }
            return variables;

        }

    }

}
