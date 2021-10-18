using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;
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

        public async Task<CompanyGeneralInfo> GetCompanyInfo(string companyId, string userId)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoProcedureRequest(companyId, userId);         
            var pfyService = new PersonifyDataService(_httpClient);
            var response = await pfyService.MakeRequest(spRequestBuilder.CreateSelectProcedureRequest());
            return null;           
        }

        public async Task<bool> UpdateAboutUs(string companyId, string aboutUs, string userId)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoAboutUsProcedureRequest(companyId, aboutUs, userId);
            var pfyService = new PersonifyDataService(_httpClient);
            var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;           
        }

        public async Task<bool> UpdateBusinessHours(string asiNumber, string businessHours, string userId)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoBusinessHoursProcedureRequest(asiNumber, businessHours, userId);
            var pfyService = new PersonifyDataService(_httpClient);
            var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;
        }

        public async Task<bool> UpdateMinorityOwned(string asiNumber, char femaleOwned, char veteranOwned, char asianOwned, char hispanic_owned,
            char african_american_owned, char native_american_owned, char jewish_owned, char disabled_owned, char esop, char cert_available,
            char small_disadvantage, char lgbtq_owned, string user_id)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoMinorityOwnedProcedureRequest(asiNumber, femaleOwned, veteranOwned, asianOwned,
                hispanic_owned, african_american_owned, native_american_owned, jewish_owned, disabled_owned, esop,
                cert_available, small_disadvantage, lgbtq_owned, user_id);
            var pfyService = new PersonifyDataService(_httpClient);
            var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;
        }

        public async Task<bool> UpdateNumberOfEmployees(string asiNumber, string Number_Of_Employees, string userId)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoNumberOfEmployeesProcedureRequest(asiNumber, Number_Of_Employees, userId);
            var pfyService = new PersonifyDataService(_httpClient);
            var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;
        }

        public async Task<bool> UpdateUpdateYearEstablished(string asiNumber, string year_established, string userId)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoYearEstablishedProcedureRequest(asiNumber, year_established, userId);
            var pfyService = new PersonifyDataService(_httpClient);
            var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;
        }        
    }
}
