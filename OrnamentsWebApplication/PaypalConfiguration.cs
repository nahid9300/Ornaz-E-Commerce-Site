using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace OrnamentsWebApplication
{
    public static class PaypalConfiguration
    {
        //Variables for storing the clientID and clientSecret key
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        //Constructor
        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId ="AbHeh1C9xek4lj8p9PqYwKX2tcf3OHOUd78ct0YpYJZLCYK9yIR_eEPth9KYTUXpk6_GuHYzRx61d_0r";
            ClientSecret ="EP0APrdx6MYrvXOtuoSkgd_JITormqUmw6MQcPl-n4Ipwwt3tljBcdViQIcRz4CPuR8CiIbYZI8n8p1W";
        }

        // getting properties from the web.config
        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            // getting accesstocken from paypal               
            string accessToken = new OAuthTokenCredential
                (ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}