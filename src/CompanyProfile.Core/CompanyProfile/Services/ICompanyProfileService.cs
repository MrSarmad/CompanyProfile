using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProfile.Core.CompanyProfile
{
    public interface ICompanyProfileService
    {
        public Task<CompanyGeneralInfo> GetCompanyInfo(string asiNumber, string subCompanyId, string userId);
        Task<bool> UpdateAboutUs(string asiNumber, string aboutUs, string userId);
        Task<bool> UpdateBusinessHours(string asiNumber, string businessHours, string userId);
        Task<bool> UpdateMinorityOwned(string asiNumber, char femaleOwned, char veteranOwned, char asianOwned,
            char hispanic_owned, char african_american_owned, char native_american_owned, char jewish_owned, char disabled_owned,
            char esop, char cert_available, char small_disadvantage, char lgbtq_owned, string user_id);
        Task<bool> UpdateNumberOfEmployees(string asiNumber, string Number_Of_Employees, string userId);
        Task<bool> UpdateUpdateYearEstablished(string asiNumber, string year_established, string userId);       
    }
}
