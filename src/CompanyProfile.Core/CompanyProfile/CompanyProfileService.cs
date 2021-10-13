using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using CompanyProfile.Core.CompanyProfile;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProfile.Core.CompanyProfile
{
    public class CompanyProfileService : ICompanyProfileService
    {
        private readonly HttpClient _httpClient;
        public CompanyProfileService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<CompanyGeneralInfo> GetCompanyInfo(string asiNumber)
        {
            IRequestProcessor processor = new GeneralRequestProcessor(asiNumber);
            var request = processor.CreateRequest();
            var pfyService = new PersonifyDataService(_httpClient);
            var response = await pfyService.MakeRequest<string>(request);
            return null;
        }
    }
}
