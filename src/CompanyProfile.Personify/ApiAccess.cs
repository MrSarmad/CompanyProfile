using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProfile.Personify
{
    public class ApiAccess : IApiAccess
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBase = "https://asi-prsw-02.uat-asicentral.com/PersonifyDataServices/PersonifyDataASI.svc/GetStoredProcedureDataXML"; 

        public ApiAccess(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        /// <summary>
        /// just a test method
        /// </summary>
        public async Task<string> GetBasicInfo()
        {
            var resp = await GetDataFromPSFY();
            return resp;
        }

        private async Task<string> GetDataFromPSFY()
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("PersonifyClient");
                httpClient.BaseAddress = new Uri(_apiBase);
                httpClient.DefaultRequestHeaders.Clear();
                //create basic auth
                var username = "webuser";
                var password = "webuser2013";

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}")));

                var paramList = new Dictionary<string, string>
                {
                    {"@ip_asi_number",  "33020"}
                };

                var xmlPostData = GetXmlPostData("USR_EASI_CUSTOMER_SEARCH_ASI_NO_PROC", paramList);

                var response = await httpClient.PostAsync("", xmlPostData);

                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch {
                return string.Empty; 
            }

        }

        private HttpContent GetXmlPostData(string procName, Dictionary<string, string> paramList)
        {
            return new StringContent("<StoredProcedureRequest> " +
                    "<StoredProcedureName>" + procName + "</StoredProcedureName> " +
                    "<IsUserDefinedFunction>false</IsUserDefinedFunction>" +
                    "<IsUDFScalar>true</IsUDFScalar> " +
                    "<SPParameterList>" +
                    buildparameterlist(paramList) + 
                    "</SPParameterList>" +
                    "</StoredProcedureRequest>",
                    System.Text.Encoding.UTF8,
                    "text/xml");

        }

        private string buildparameterlist(Dictionary<string, string> parameterList)
        {
            var sb = new StringBuilder(); 
            foreach (var keyValuePair in parameterList)
            {
                sb.Append(
                "<StoredProcedureParameter>" +
                "<Name>" + keyValuePair.Key + "</Name>" +
                "<Value>" + keyValuePair.Value + "</Value>" +
                "<Direction>1</Direction>" +
                "</StoredProcedureParameter>"); 


            }
            return sb.ToString(); 
        }
    }
}
