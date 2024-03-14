using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Core;

namespace DynamoJsonCreator
{
    class Authentication : IDSDKManager
    {
        private static string token = null;
        /// <summary>
        /// Get token after authentication
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            IDSDKManager iDSDKManager = new IDSDKManager();
            var login = iDSDKManager.Login();
            Authentication.token = iDSDKManager.GetAccessToken();

            return token;
        }
    }
}
