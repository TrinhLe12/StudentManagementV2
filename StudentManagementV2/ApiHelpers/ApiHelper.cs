using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagementV2.ApiHelpers
{
    public class ApiHelper
    {
        private static readonly string baseUrl = "https://localhost:44369/api";

        public static async Task<string> GetAll (string url)
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteGetAsync(request);
            return response.Content;
        }

        public static async Task<string> GetById(string url)
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteGetAsync(request);
            return response.Content;
        }

        public static async Task<string> Delete(string url)
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(url, Method.Delete);
            var response = await client.DeleteAsync(request);
            return response.Content;
        }

        public static async Task<string> Create<T>(string url, T entity) where T : class
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddBody(entity);
            var response = await client.PostAsync(request);
            return response.Content;
        }

        public static async Task<string> Update<T> (string url, T entity) where T : class
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(url, Method.Put);
            request.AddBody(entity);
            var response = await client.PutAsync(request);
            return response.Content;
        }

        public static async Task<string> Search(string url)
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteGetAsync(request);
            return response.Content;
        }
    }
}
