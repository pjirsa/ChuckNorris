using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ChuckNorris.Api
{
    public class ChuckNorrisModule : NancyModule
    {
        private const string BASE_API = "http://api.icndb.com/";

        public ChuckNorrisModule()
        {
            Get["/"] = _ =>
                {
                    return "Hello from Chuck Norris API.";
                };

            Get["/jokes/{id:int}"] = _ =>
                {
                    return GetResponse(BASE_API + "jokes/" + _.id);
                };

            Get["/jokes"] = _ => 
            {
                string reqUrl = GetRequestUrl("jokes", Request.Query);
                return GetResponse(reqUrl);
            };

            Get["/jokes/random"] = _ =>
                {
                    string reqUrl = GetRequestUrl("jokes/random", Request.Query);
                    return GetResponse(reqUrl);
                };
        }

        #region Helpers

        private string GetRequestUrl(string endpoint, DynamicDictionary query)
        {
            string reqUrl = BASE_API + endpoint;
            if (Request.Query.Count > 0)
            {
                reqUrl += "?";
                foreach (var qs in Request.Query)
                {
                    reqUrl += qs + "=" + Request.Query[qs] + "&";
                }
            }
            return reqUrl;
        }

        private Response GetResponse(string requestUrl)
        {
            var req = WebRequest.CreateHttp(requestUrl);
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            string json = reader.ReadToEnd();
            var result = (Response)json;
            result.ContentType = "application/json";
            return result;
        }

        #endregion
    }
}