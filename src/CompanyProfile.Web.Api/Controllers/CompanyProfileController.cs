using ASI.Contracts.CompanyProfile.CompanyProfile.DTO;
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
            var companyInfo = new DTOBase { CompanyId = companyId };
            return await _service.GetCompanyInfo(companyInfo);
        }

        [HttpPut]
        [Route("UpdateAboutUs")]
        public async Task<bool> UpdateAboutUs(AboutUsDTO dto)
        {
            return await _service.UpdateAboutUs(dto);
        }

        [HttpPut]
        [Route("UpdateBusinessHours")]
        public async Task<bool> UpdateBusinessHours(BusinessHoursDTO dto)
        {           
            return await _service.UpdateBusinessHours(dto);
        }

        [HttpPut]
        [Route("UpdateMinorityOwned")]
        public async Task<bool> UpdateMinorityOwned(MinorityOwnedDTO dto)
        {            
            return await _service.UpdateMinorityOwned(dto);
        }


        [HttpPut]
        [Route("UpdateNumberOfEmployees")]
        public async Task<bool> UpdateNumberOfEmployees(NumberOfEmployeesDTO dto)
        {
            return await _service.UpdateNumberOfEmployees(dto);
        }


        [HttpPut]
        [Route("UpdateUpdateYearEstablished")]
        public async Task<bool> UpdateUpdateYearEstablished(YearEstablishedDTO dto)
        {
            return await _service.UpdateUpdateYearEstablished(dto);
        }       
    }
}
