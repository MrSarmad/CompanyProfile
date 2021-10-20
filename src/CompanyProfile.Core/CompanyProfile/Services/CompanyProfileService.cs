﻿using ASI.Contracts.CompanyProfile.CompanyProfile.DTO;
using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using CompanyProfile.Personify;

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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiAccess _pfyService;

        public CompanyProfileService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _pfyService = new ApiAccess(httpClientFactory);
        }        

        public async Task<CompanyGeneralInfo> GetCompanyInfo(DTOBase dto)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoProcedureRequest(dto.CompanyId, dto.UserId);
            var request = spRequestBuilder.CreateSelectProcedureRequest();
            var response = await _pfyService.GetGeneralInfo(request);
            return response;
        }

        public async Task<bool> UpdateAboutUs(AboutUsDTO dto)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoAboutUsProcedureRequest(dto.CompanyId, dto.AboutUs, dto.UserId);
            var pfyService = new PersonifyDataService(_httpClient);
            //var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;           
        }

        public async Task<bool> UpdateBusinessHours(BusinessHoursDTO dto)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoBusinessHoursProcedureRequest(dto.CompanyId, dto.BusinessHours, dto.UserId);
            var pfyService = new PersonifyDataService(_httpClient);
            //var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;
        }

        public async Task<bool> UpdateMinorityOwned(MinorityOwnedDTO dto)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoMinorityOwnedProcedureRequest(dto.CompanyId, dto.FemaleOwned, dto.VeteranOwned, dto.AsianOwned,
                dto.HispanicOwned, dto.AfricanAmericanOwned, dto.NativeAmericanOwned, dto.JewishOwned, dto.DisabledOwned, dto.Esop,
                dto.CertAvailable, dto.SmallDisadvantage, dto.LgbtqOwned, dto.UserId);
            var pfyService = new PersonifyDataService(_httpClient);
            //var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;
        }

        public async Task<bool> UpdateNumberOfEmployees(NumberOfEmployeesDTO dto)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoNumberOfEmployeesProcedureRequest(dto.CompanyId, dto.Number_Of_Employees, dto.UserId);
            var pfyService = new PersonifyDataService(_httpClient);
            //var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;
        }

        public async Task<bool> UpdateUpdateYearEstablished(YearEstablishedDTO dto)
        {
            IProcedureRequest spRequestBuilder = new GeneralInfoYearEstablishedProcedureRequest(dto.CompanyId, dto.YearEstablished, dto.UserId);
            var pfyService = new PersonifyDataService(_httpClient);
            //var response = await pfyService.MakeRequest(spRequestBuilder.CreateUpdateProcedureRequest());
            return true;
        }        
    }
}
