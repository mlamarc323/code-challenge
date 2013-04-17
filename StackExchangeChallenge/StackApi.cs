using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace WebApplication2
{
    public class StackApi
    {
        private const string ApiVersion = "2.1";
        private const string BaseUri = "https://api.stackexchange.com/" + ApiVersion;
        
        private readonly string _apiKey;

        public StackApi()
        {
            this._apiKey = string.Empty;
        }

        public int? MaxRateLimit { get; set; }
        public int? CurrentRateLimit { get; set; }

        // Concatenates base Uri
        private string ComposeUri(string path)
        {
            var uri = String.Format("{0}{1}", BaseUri, path);
            if (!String.IsNullOrWhiteSpace(this._apiKey))
            {
                var separator = uri.Contains("?") ? "&" : "?";
                uri = String.Format("{0}{1}key={2}", uri, separator, this._apiKey);
            }
            return uri;
        }

        // Retrieves json data
        private string GetResponse(string requestUri)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";
            var json = ExtractJsonResponse(request.GetResponse());
            return json;
        }

        // Reading json to preparte for parse
        private string ExtractJsonResponse(WebResponse response)
        {
            ParseHeaders(response);

            string json;
            using (var outStream = new MemoryStream())
            using (var zipStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
            {
                zipStream.CopyTo(outStream);
                outStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(outStream, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
            }
            return json;
        }

        // Finds json headers and parses
        private void ParseHeaders(WebResponse response)
        {
            if (response.Headers == null) return;

            if (response.Headers.AllKeys.Contains("X-RateLimit-Max"))
            {
                this.MaxRateLimit = Int32.Parse(response.Headers["X-RateLimit-Max"]);
            }
            if (response.Headers.AllKeys.Contains("X-RateLimit-Current"))
            {
                this.CurrentRateLimit = Int32.Parse(response.Headers["X-RateLimit-Current"]);
            }
        }

        // Parses json data
        private static IEnumerable<T> ParseJson<T>(string json) where T : class, new()
        {
            var type = typeof(T);
            var attribute = type.GetCustomAttributes(typeof(WrapperObjectAttribute), false).SingleOrDefault() as WrapperObjectAttribute;
            if (attribute == null)
            {
                throw new InvalidOperationException(
                    String.Format("{0} type must be decorated with a WrapperObjectAttribute.", type.Name));
            }

            var jobject = JObject.Parse(json);
            var collection = JsonConvert.DeserializeObject<List<T>>(jobject[attribute.WrapperObject].ToString());
            return collection;
        }

        // creating full url to retrieve data and encapsulate output in an object
        private T GetStackExchangeObject<T>(string path) where T : class, new()
        {
            var requestUri = ComposeUri(path);
            var json = GetResponse(requestUri);
            return ParseJson<T>(json).FirstOrDefault();
        }

        private IEnumerable<T> GetStackExchangeObjects<T>(string path) where T : class, new()
        {
            var requestUri = ComposeUri(path);
            var json = GetResponse(requestUri);
            return ParseJson<T>(json);
        }

        // retrieves record with highest score
        public Item GetHighestScore()
        {
            return GetStackExchangeObject<Item>("/questions?page=1&pagesize=1&sort=votes&site=stackoverflow");
        }

        // Get all Users
        public IEnumerable<Item> GetAllUsers()
        {
            IEnumerable<Item> users = GetStackExchangeObjects<Item>("/users?site=stackoverflow");
            return users;
        }

        // Get all Questions
        public IEnumerable<Item> GetAllQuestions()
        {
            IEnumerable<Item> questions = GetStackExchangeObjects<Item>("/questions?site=stackoverflow");
            return questions;
        }
        
        // Display the sum of the reputation for all users in that request
        public string GetReputationSum()
        {
            IEnumerable<Item> users = GetAllUsers();
            string repSum = users.Sum(w => w.Reputation).ToString();
            return repSum;
        }

        // Display the user with highest reputation
        public Item GetHighestReputationUser()
        {
            IEnumerable<Item> users = GetAllUsers();
            Item user = users.OrderByDescending(w => w.Reputation).First();
            return user;
        }

        
    }
}