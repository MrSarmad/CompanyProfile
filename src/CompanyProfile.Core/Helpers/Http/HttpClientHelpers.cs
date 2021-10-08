using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProfile.Core.Http
{
    public sealed class HttpClientHelper
    {
        private readonly HttpClient _client;

        public HttpClientHelper(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<T> GetAsync<T>(string path)
        {
            var res = await _client.GetAsync(path);
            await HttpHelpers.HandleStatusCode(path, res, "GET");
            var str = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }

        public async Task<T> PostAsync<T>(string path, object o)
        {
            string json = JsonConvert.SerializeObject(o);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(path, content);
            await HttpHelpers.HandleStatusCode(path, res, "POST");
            var str = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }

        public async Task<T> PutAsync<T>(string path, object o)
        {
            string json = JsonConvert.SerializeObject(o);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PutAsync(path, content);
            await HttpHelpers.HandleStatusCode(path, res, "PUT");
            var str = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }

        public async Task<string> DeleteAsync(string path)
        {
            var res = await _client.DeleteAsync(path);
            await HttpHelpers.HandleStatusCode(path, res, "DELETE");
            var str = await res.Content.ReadAsStringAsync();
            return str;
        }       
    }
}
