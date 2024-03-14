using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSVCreator
{
    public class Data
    {
        /// <summary>
        /// GetsHttpResponse from json
        /// </summary>
        /// <returns></returns>
        public static string GetHtpResponse()
        {
            var response = File.ReadAllText(@"D:\DynamoJsonCreator\CSVCreator\ExchangeData.json");
            var unescapedJson = JToken.Parse(response).ToString();
            return unescapedJson;
        }
        /// <summary>
        /// Gets HttpClient
        /// </summary>
        /// <returns></returns>
        public static HttpClient GetHttpClient()
        {
            string content_string = GetHtpResponse();
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content_string, Encoding.UTF8, "application/json")
                });
            var httpClient = new HttpClient(handler.Object);
            return httpClient;
        }
    }
}
