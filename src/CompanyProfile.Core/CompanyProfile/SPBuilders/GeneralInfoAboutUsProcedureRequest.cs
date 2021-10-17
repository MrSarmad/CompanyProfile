using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public class GeneralInfoAboutUsProcedureRequest : ProcedureRequestBase
    {
        public GeneralInfoAboutUsProcedureRequest(string asiNumber, string subCompanyId, string userId)
        {
            CompanyId = GetAsiNumber(asiNumber);
            SubCompanyId = subCompanyId;
            UserId = userId;
        }

        public override StoredProcedureRequest CreateSelectProcedureRequest()
        {
            throw new NotImplementedException();
        }

        public override StoredProcedureRequest CreateInsertProcedureRequest()
        {
            throw new NotImplementedException();
        }

        public override StoredProcedureRequest CreateUpdateProcedureRequest()
        {
            SPName = ProceduresInfo.USR_CPI_General_Update_About_Us.SPNAME;
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_About_Us.ip_usr_customer_number, Value = CompanyId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_About_Us.ip_about_us, Value = SubCompanyId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_About_Us.@ip_user_id, Value = UserId, Direction = 1 });
            return CreateRequest();
        }

        public override StoredProcedureRequest CreateDeleteProcedureRequest()
        {
            throw new NotImplementedException();
        }
    }    
}
