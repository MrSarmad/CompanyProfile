using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;
using CompanyProfile.Core.CompanyProfile;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProfile.Web.Api.Controllers
{
    [Route("api/company")]
    public class CompanyProfileController : Controller
    {
        private readonly ICompanyProfileService _service;

        public CompanyProfileController(ICompanyProfileService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("GeneralInfo")]
        public async Task<CompanyGeneralInfo> GetGeneralInfo(string companyId)
        {
            return await _service.GetCompanyInfo(companyId, GetCurrentUserName());
        }

        [HttpPut]
        [Route("UpdateAboutUs")]
        public async Task<bool> UpdateAboutUs(string companyId, string aboutUs)
        {
            return await _service.UpdateAboutUs(companyId, aboutUs, GetCurrentUserName());
        }

        [HttpPut]
        [Route("UpdateBusinessHours")]
        public async Task<bool> UpdateBusinessHours(string asiNumber, string businessHours, string userId)
        {           
            return await _service.UpdateBusinessHours(asiNumber, businessHours, userId);
        }



        [HttpPut]
        [Route("UpdateMinorityOwned")]
        public async Task<bool> UpdateMinorityOwned(string asiNumber, char femaleOwned, char veteranOwned, char asianOwned,
            char hispanic_owned, char african_american_owned, char native_american_owned, char jewish_owned, char disabled_owned,
            char esop, char cert_available, char small_disadvantage, char lgbtq_owned, string user_id)
        {            
            return await _service.UpdateMinorityOwned(asiNumber, femaleOwned, veteranOwned, asianOwned,
            hispanic_owned, african_american_owned,  native_american_owned, jewish_owned, disabled_owned,
            esop, cert_available, small_disadvantage, lgbtq_owned, user_id);
        }


        [HttpPut]
        [Route("UpdateNumberOfEmployees")]
        public async Task<bool> UpdateNumberOfEmployees(string asiNumber, string Number_Of_Employees, string userId)
        {
            return await _service.UpdateNumberOfEmployees(asiNumber, Number_Of_Employees, userId);
        }


        [HttpPut]
        [Route("UpdateUpdateYearEstablished")]
        public async Task<bool> UpdateUpdateYearEstablished(string asiNumber, string year_established, string userId)
        {
            return await _service.UpdateUpdateYearEstablished(asiNumber, year_established, userId);
        }

        private string GetCurrentUserName()
        {
            return "CURWILER";
        }
    }
}
