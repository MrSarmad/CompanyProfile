using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public class GeneralInfoProcedureRequest : ProcedureRequestBase
    {
        public GeneralInfoProcedureRequest(string asiNumber, string subCompanyId, string userId)
        {
            CompanyId = GetAsiNumber(asiNumber);
            SubCompanyId = subCompanyId;
            UserId = userId;
        }

        public override StoredProcedureRequest CreateSelectProcedureRequest()
        {
            SPName = ProceduresInfo.USR_CPI_General_Select.SPNAME;
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Select.@ip_master_customer_id, Value = CompanyId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Select.@ip_sub_customer_id, Value = SubCompanyId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Select.@ip_user_id, Value = UserId, Direction = 1 });
            return CreateRequest();
        }

        public override StoredProcedureRequest CreateInsertProcedureRequest()
        {
            throw new NotImplementedException();
        }

        public override StoredProcedureRequest CreateUpdateProcedureRequest()
        {
            throw new NotImplementedException();
        }

        public override StoredProcedureRequest CreateDeleteProcedureRequest()
        {
            throw new NotImplementedException();
        }
    }    
}
