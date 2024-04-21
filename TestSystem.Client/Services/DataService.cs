using TestSystem.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using TestSystem.Core.Models;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace TestSystem.Client.Services
{
    public class DataService<T> : IDataService<T> where T : BaseFile
    {
        private readonly HttpClient _httpClient;
        private string _host;

        public DataService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _host = configuration.GetSection("Host").Value ?? string.Empty;
        }
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel = default)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<T>>($"{_host}/all", Cancel)
                .ConfigureAwait(false) ?? Enumerable.Empty<T>();
        }

        public async Task<T?> SendDataAsync(T item, CancellationToken Cancel = default)
        {
            var json = JsonSerializer.Serialize(item);

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(json), "json");

            var imageBytes = File.ReadAllBytes(item.FilePath);
            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            multipartContent.Add(imageContent, "image", Path.GetFileName(Path.GetFileName(item.FilePath)));

            var response = await _httpClient.PostAsync($"{_host}/img", multipartContent, Cancel)
                .ConfigureAwait(false);
            var result = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<T>(cancellationToken: Cancel)
               .ConfigureAwait(false);
            return result;

        }
    }
}
