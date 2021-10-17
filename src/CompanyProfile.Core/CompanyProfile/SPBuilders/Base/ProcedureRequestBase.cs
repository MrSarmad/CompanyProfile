using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public abstract class ProcedureRequestBase : IProcedureRequest
    {
        public string SPName { get; set; }
        public string CompanyId { get; set; }
        public string SubCompanyId { get; set; }
        public string UserId { get; set; }
        protected List<StoredProcedureRequestParameter> RequestParams { get; set; }

        public abstract StoredProcedureRequest CreateSelectProcedureRequest();
        public abstract StoredProcedureRequest CreateInsertProcedureRequest();
        public abstract StoredProcedureRequest CreateUpdateProcedureRequest();
        public abstract StoredProcedureRequest CreateDeleteProcedureRequest();

        public StoredProcedureRequest CreateRequest()
        {
            var request = new StoredProcedureRequest
            {
                StoredProcedureName = SPName,
                IsUDFScalar = true,
                IsUserDefinedFunction = false,
                SPParameterList = new StoredProcedureRequestParameter[] { }
            };

            request.SPParameterList = RequestParams.ToArray();
            return request;
        }

        protected string GetAsiNumber(string asiNumber)
        {
            if (!string.IsNullOrEmpty(asiNumber))
            {
                return asiNumber.PadLeft(12, '0');
            }
            return string.Empty;
        }
    }    
}
