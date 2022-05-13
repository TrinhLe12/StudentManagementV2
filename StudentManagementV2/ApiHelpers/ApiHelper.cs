using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using StudentManagementV2.Core.PaginatedLists;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<PaginatedList<T>> GetAllPaging<T>(string url) where T : class 
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteGetAsync<PaginatedList<T>>(request);

            var headers = response.Headers;
            int totalPages = 0;
            int pageIndex = 0;

            foreach (var header in headers) {
                if (header.Name.Equals("TotalPages"))
                {
                    totalPages = Int32.Parse(header.Value.ToString());
                    continue;
                } 
                
                if (header.Name.Equals("PageIndex"))
                {
                    pageIndex = Int32.Parse(header.Value.ToString());
                    continue;
                }
            }

            PaginatedList<T> result = new PaginatedList<T>(response.Data, totalPages, pageIndex);

            return result;
            
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
