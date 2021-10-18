using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public class GeneralInfoAboutUsProcedureRequest : ProcedureRequestBase
    {
        public string AboutUsInfo { get; set; }
        public GeneralInfoAboutUsProcedureRequest(string companyId, string aboutUsInfo, string userId)
        {
            CompanyId = companyId;
            UserId = userId;
            AboutUsInfo = aboutUsInfo;
            RequestParams = new List<StoredProcedureRequestParameter>();
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
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_About_Us.ip_user_id, Value = UserId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_About_Us.ip_about_us, Value = AboutUsInfo, Direction = 1 });
            return CreateRequest();
        }

        public override StoredProcedureRequest CreateDeleteProcedureRequest()
        {
            throw new NotImplementedException();
        }
    }    
}
