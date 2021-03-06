using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public partial class ProceduresInfo
    {
        public class USR_CPI_General_Update_Business_Hours
        {
            public const string SPNAME = "USR_CPI_General_Update_Business_Hours ";
            public const string ip_usr_customer_number = "@ip_usr_customer_number";
            public const string ip_business_hours = "@ip_business_hours";
            public const string ip_user_id = "@ip_user_id";
        }
    }

    public class GeneralInfoBusinessHoursProcedureRequest : ProcedureRequestBase
    {
        private string BusinessHours { get; set; }
        public GeneralInfoBusinessHoursProcedureRequest(string asiNumber, string businessHours, string userId)
        {
            CompanyId = GetAsiNumber(asiNumber);
            BusinessHours = businessHours;
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
            SPName = ProceduresInfo.USR_CPI_General_Update_Business_Hours.SPNAME;
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Business_Hours.ip_usr_customer_number, Value = CompanyId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Business_Hours.ip_business_hours, Value = BusinessHours, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Business_Hours.ip_user_id, Value = UserId, Direction = 1 });
            return CreateRequest();
        }

        public override StoredProcedureRequest CreateDeleteProcedureRequest()
        {
            throw new NotImplementedException();
        }
    }    
}
