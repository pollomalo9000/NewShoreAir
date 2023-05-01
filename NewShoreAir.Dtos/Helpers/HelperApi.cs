using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewShoreAir.Domain.Helpers
{
    public static class HelperApi
    {


        public async static Task<RestResponse> Get<T>(string apiEndPoint, string enumApi)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var client = new RestClient(GetUrl(enumApi));
            var request = new RestRequest(apiEndPoint);
            var response = await client.ExecuteGetAsync<T>(request);
            return response;
        }







        private static string GetUrl(string enumApi)
        {
            return HelperConfiguracion.config.GetSection($"Apis:{enumApi}:Url").Value;
        }




    }
}
