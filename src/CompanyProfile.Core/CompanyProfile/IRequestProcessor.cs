using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public interface IRequestProcessor
    {
        string CompanyId { get; set; }
        string SubCompanyId { get; set; }
        string UserId { get; set; }

        StoredProcedureRequest CreateRequest();
    }
}
