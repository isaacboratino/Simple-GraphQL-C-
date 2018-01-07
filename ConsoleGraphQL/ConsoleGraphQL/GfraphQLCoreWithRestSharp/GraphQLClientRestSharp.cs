using System;
using RestSharp;
using System.Net;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace GfraphQLCoreWithRestSharp
{
    class GraphQLClientRestSharp
    {
        private RestClient _client;

        public GraphQLClientRestSharp(string GraphQLApiUrl)
        {
            _client = new RestClient(GraphQLApiUrl);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public dynamic Execute(string query, object variables = null, Dictionary<string, string> additionalHeaders = null)
        {
            var request = new RestRequest("/", Method.POST);

            if (additionalHeaders != null && additionalHeaders.Count > 0)
            {
                foreach (var additionalHeader in additionalHeaders)
                {
                    request.AddHeader(additionalHeader.Key, additionalHeader.Value);
                }
            }

            request.AddJsonBody(new
            {
                query = query,
                variables = variables
            });

            return JObject.Parse(_client.Execute(request).Content);
        }
    }
}