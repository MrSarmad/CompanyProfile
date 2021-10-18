using ASI.Contracts.CompanyProfile.CompanyProfile.DTO;
using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProfile.Core.CompanyProfile
{
    public interface ICompanyProfileService
    {
        public Task<CompanyGeneralInfo> GetCompanyInfo(DTOBase dto);
        Task<bool> UpdateAboutUs(AboutUsDTO dto);
        Task<bool> UpdateBusinessHours(BusinessHoursDTO dto);
        Task<bool> UpdateMinorityOwned(MinorityOwnedDTO dto);
        Task<bool> UpdateNumberOfEmployees(NumberOfEmployeesDTO dto);
        Task<bool> UpdateUpdateYearEstablished(YearEstablishedDTO dto);       
    }
}
