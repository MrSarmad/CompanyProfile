using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public interface IProcedureRequest
    {
        public string SPName { get; set; }
        public string CompanyId { get; set; }
        public string SubCompanyId { get; set; }
        public string UserId { get; set; }

        public abstract StoredProcedureRequest CreateSelectProcedureRequest();
        public abstract StoredProcedureRequest CreateInsertProcedureRequest();
        public abstract StoredProcedureRequest CreateUpdateProcedureRequest();
        public abstract StoredProcedureRequest CreateDeleteProcedureRequest();
    }    
}
