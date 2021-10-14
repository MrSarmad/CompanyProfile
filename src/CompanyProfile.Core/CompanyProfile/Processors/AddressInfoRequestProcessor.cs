using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public class AddressInfoRequestProcessor : IRequestProcessor
    {
        public string CompanyId { get; set; }
        public string SubCompanyId { get; set; }
        public string UserId { get; set; }
        private Dictionary<string, string> requestParams { get; set; }

        public AddressInfoRequestProcessor(string asiNumber)
        {
            CompanyId = GetAsiNumber(asiNumber);
            SubCompanyId = "0";
            UserId = GetCurrentUserName();
            LoadRequestParams();
        }

        public StoredProcedureRequest CreateRequest()
        {
            var req = new StoredProcedureRequest
            {
                StoredProcedureName = "USR_CPI_Address_Select",
                IsUDFScalar = true,
                IsUserDefinedFunction = false,
                SPParameterList = new StoredProcedureRequestParameter[] { }
            };

            var paramList = new List<StoredProcedureRequestParameter>();

            foreach (var kvp in requestParams)
            {
                var newParam = new StoredProcedureRequestParameter
                {
                    Name = kvp.Key,
                    Direction = 1,
                    Value = kvp.Value
                };

                paramList.Add(newParam);

            }
            req.SPParameterList = paramList.ToArray();

            return req;
        }

        private string GetAsiNumber(string asiNumber)
        {
            if (!string.IsNullOrEmpty(asiNumber))
            {
                return asiNumber.PadLeft(12, '0');
            }
            return string.Empty;
        }

        private void LoadRequestParams()
        {
            requestParams.Clear();
            requestParams.Add("@ip_master_customer_id", CompanyId);
            requestParams.Add("@ip_sub_customer_id", SubCompanyId);
            requestParams.Add("@ip_user_id", UserId);
        }

        private string GetCurrentUserName()
        {
            return "CURWILER";
        }
    }
}
