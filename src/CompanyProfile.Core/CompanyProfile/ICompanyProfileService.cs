using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProfile.Core.CompanyProfile
{
    public interface ICompanyProfileService
    {
        Task<CompanyGeneralInfo> GetCompanyInfo(string asiNumber);
        Task<CompanyGeneralInfo> GetAddressInfo(string asiNumber);
    }
}
