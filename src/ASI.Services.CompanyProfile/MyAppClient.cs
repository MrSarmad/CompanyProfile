using ASI.Contracts.CompanyProfile;
using ASI.Contracts.CompanyProfile.Search;
using ASI.Services.CompanyProfile.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Services.CompanyProfile
{
    public class CompanyProfileClient : ICompanyProfileClient
    {
        public const string HttpClientName = "CompanyProfile";

        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient? _httpClient;
        private HttpClient HttpClient => _httpClient ??= _httpClientFactory.CreateClient(HttpClientName);

        public CompanyProfileClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public virtual async Task<MyEntityView> AddMyEntityAsync(MyEntityView view)
        {
            var relativeuri = $"api/myentities";
            var json = JsonConvert.SerializeObject(view);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await HttpClient.PostAsync(relativeuri, content).ConfigureAwait(false);

            await HttpHelpers.HandleResponseErrors(relativeuri, responseMessage).ConfigureAwait(false);

            var responseContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<MyEntityView>(responseContent);
        }

        public virtual async Task<MyEntityView> GetMyEntityAsync(long id)
        {
            var relativeuri = $"api/myentities/{id}";
            var responseMessage = await HttpClient.GetAsync(relativeuri).ConfigureAwait(false);

            await HttpHelpers.HandleResponseErrors(relativeuri, responseMessage).ConfigureAwait(false);

            var responseContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<MyEntityView>(responseContent);
        }

        public virtual async Task<SearchResultView<MyEntitySearchView>> SearchAsync(SearchCriteriaView request)
        {
            var relativeuri = $"api/myentities/search";
            var reqContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var responseMessage = await HttpClient.PostAsync(relativeuri, reqContent).ConfigureAwait(false);

            await HttpHelpers.HandleResponseErrors(relativeuri, responseMessage).ConfigureAwait(false);

            var responseContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<SearchResultView<MyEntitySearchView>>(responseContent);
        }
    }
}
