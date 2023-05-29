using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content, T defaultValue = default)
        {
            try
            {
                if (content == null) return defaultValue;
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string json = await content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetBytes(json), options);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}