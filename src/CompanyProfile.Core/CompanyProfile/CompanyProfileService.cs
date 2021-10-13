using ASI.Contracts.CompanyProfile.XMLModel;

using CompanyProfile.Core.CompanyProfile;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProfile.Core.CompanyProfile
{
    public class CompanyProfileService : ICompanyProfileService
    {
        public CompanyProfileService()
        {

        }

        public Task<CompanyGeneralInfo> GetCompanyInfo(string asiNumber)
        {
            IRequestProcessor processor = new GeneralRequestProcessor(asiNumber);
            var request = processor.CreateRequest();
            return null;
        }
    }
}
