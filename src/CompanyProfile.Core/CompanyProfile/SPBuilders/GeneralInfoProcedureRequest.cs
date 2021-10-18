using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public partial class ProceduresInfo
    {
        public class USR_CPI_General_Select
        {
            public const string SPNAME = "USR_CPI_General_Select";
            public const string ip_usr_customer_number = "@ip_usr_customer_number";
            public const string ip_user_id = "@ip_user_id";
        }
    }

    public class GeneralInfoProcedureRequest : ProcedureRequestBase
    {
        public GeneralInfoProcedureRequest(string companyId, string userId)
        {
            CompanyId = companyId;
            UserId = userId;
            RequestParams = new List<StoredProcedureRequestParameter>();
        }

        public override StoredProcedureRequest CreateSelectProcedureRequest()
        {
            SPName = ProceduresInfo.USR_CPI_General_Select.SPNAME;
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Select.ip_usr_customer_number, Value = CompanyId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Select.ip_user_id, Value = UserId, Direction = 1 });
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
