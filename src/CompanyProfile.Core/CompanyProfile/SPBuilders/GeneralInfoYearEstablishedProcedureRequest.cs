using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public class GeneralInfoYearEstablishedProcedureRequest : ProcedureRequestBase
    {
        private string Year_established { get; set; }
        public GeneralInfoYearEstablishedProcedureRequest(string asiNumber, string year_established, string userId)
        {
            CompanyId = GetAsiNumber(asiNumber);
            Year_established = year_established;
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
            SPName = ProceduresInfo.USR_CPI_General_Update_Year_Established.SPNAME;
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Year_Established.ip_usr_customer_number, Value = CompanyId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Year_Established.ip_year_established, Value = Year_established, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Year_Established.ip_user_id, Value = UserId, Direction = 1 });
            return CreateRequest();
        }

        public override StoredProcedureRequest CreateDeleteProcedureRequest()
        {
            throw new NotImplementedException();
        }
    }    
}
