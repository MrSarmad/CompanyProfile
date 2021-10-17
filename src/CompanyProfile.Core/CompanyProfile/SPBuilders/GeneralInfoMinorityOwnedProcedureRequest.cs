using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public class GeneralInfoMinorityOwnedProcedureRequest : ProcedureRequestBase
    {
        private string FemaleOwned { get; set; }
        private string VeteranOwned { get; set; }
        private string AsianOwned { get; set; }
        private string Hispanic_owned { get; set; }
        private string African_american_owned { get; set; }
        private string Native_american_owned { get; set; }
        private string Jewish_owned { get; set; }
        private string Disabled_owned { get; set; }
        private string Esop { get; set; }
        private string Cert_available { get; set; }
        private string Small_disadvantage { get; set; }
        private string Lgbtq_owned { get; set; }

        public GeneralInfoMinorityOwnedProcedureRequest(string asiNumber, char femaleOwned, char veteranOwned, char asianOwned, char hispanic_owned,
            char african_american_owned, char native_american_owned, char jewish_owned, char disabled_owned, char esop, char cert_available,
            char small_disadvantage, char lgbtq_owned, string user_id)
        {
            CompanyId = GetAsiNumber(asiNumber);           
            FemaleOwned = femaleOwned.ToString();
            VeteranOwned = veteranOwned.ToString();
            AsianOwned = asianOwned.ToString();
            Hispanic_owned = hispanic_owned.ToString();
            African_american_owned = african_american_owned.ToString();
            Native_american_owned = native_american_owned.ToString();
            Jewish_owned = jewish_owned.ToString();
            Disabled_owned = disabled_owned.ToString();
            Esop = esop.ToString();
            Cert_available = cert_available.ToString();
            Small_disadvantage = small_disadvantage.ToString();
            Lgbtq_owned = lgbtq_owned.ToString();
            UserId = user_id;
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
            SPName = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.SPNAME;
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_usr_customer_number, Value = CompanyId, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_female_owned, Value = FemaleOwned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_veteran_owned, Value = VeteranOwned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_asian_owned, Value = AsianOwned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_hispanic_owned, Value = Hispanic_owned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_african_american_owned, Value = African_american_owned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_native_american_owned, Value = Native_american_owned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_jewish_owned, Value = Jewish_owned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_disabled_owned, Value = Disabled_owned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_esop, Value = Esop, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_cert_available, Value = Cert_available, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_small_disadvantage, Value = Small_disadvantage, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_lgbtq_owned, Value = Lgbtq_owned, Direction = 1 });
            RequestParams.Add(new StoredProcedureRequestParameter { Name = ProceduresInfo.USR_CPI_General_Update_Minority_Owned.@ip_user_id, Value = UserId, Direction = 1 });
            return CreateRequest();
        }

        public override StoredProcedureRequest CreateDeleteProcedureRequest()
        {
            throw new NotImplementedException();
        }
    }    
}
