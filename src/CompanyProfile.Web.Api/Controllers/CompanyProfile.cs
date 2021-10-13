using CompanyProfile.Core.CompanyProfile;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProfile.Web.Api.Controllers
{
    [Route("api/company")]
    public class CompanyProfile : Controller
    {
        private readonly ICompanyProfileService _service;

        public CompanyProfile(CompanyProfileService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("info")]
        public async void GetGeneralInfo(string asiNumber)
        {
            
        }
    }
}
