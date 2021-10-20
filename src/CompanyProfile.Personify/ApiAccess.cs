using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;
using CompanyProfile.Personify.Helpers;
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
        private readonly string _apiUser = "webuser";
        private readonly string _apiPass = "webuser2013";
        private readonly HttpClient _httpClient;

        public ApiAccess(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            if (_httpClientFactory != null)
            {
                //var baseRadarUri = ConfigurationManager.AppSettings["_psfyBase"];
                var baseRadarUri = _apiBase;
                if (string.IsNullOrWhiteSpace(baseRadarUri))
                {
                    var exc = new Exception("invalid URL - missing RadarApiBase");
                    throw exc;
                }
                _httpClient = _httpClientFactory.CreateClient("PersonifyClient");
                _httpClient.BaseAddress = new Uri(baseRadarUri);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                                 Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_apiUser}:{_apiPass}")));
            }
        }

        public async Task<CompanyGeneralInfo> GetGeneralInfo(StoredProcedureRequest req)
        {
            CompanyGeneralInfo retVal = null;
            if (req != null)
            {
                var info = await GetFromPersonify<CompanyGeneralInfo>(req);
                if(info is CompanyGeneralInfo)
                    retVal = info;
            }

            return retVal;
        }

        private async Task<T> GetFromPersonify<T>(StoredProcedureRequest req) where T : class
        {
            var serializedXml = XmlHelper.Serialize(req);
            var psfyPostResponse = await _httpClient.PostAsync("", new StringContent(serializedXml, Encoding.UTF8, "text/xml"));
            var psfyResponseContent = await psfyPostResponse.Content.ReadAsStringAsync();

            try
            {
                var output = XmlHelper.Deserialize<StoredProcedureResponse>(psfyResponseContent, "StoredProcedureOutput");
                if (output != null && output is StoredProcedureResponse)
                {
                    var obj = (StoredProcedureResponse)output;
                    var dataNode = XmlHelper.ConvertToXML(obj.Data);
                    if (!string.IsNullOrWhiteSpace(dataNode))
                    {
                        var dataContainerObject = XmlHelper.Deserialize<StoredProcedureResponseContainer>(dataNode, "NewDataSet");
                        if (dataContainerObject != null && dataContainerObject is StoredProcedureResponseContainer)
                        {
                            var container = (StoredProcedureResponseContainer)dataContainerObject;
                            if (container.Table != null)
                            {
                                var dataTable = XmlHelper.Serialize<object>(container.Table, "Table");
                                var tableXML = XmlHelper.ConvertToXML(dataTable);
                                var response = XmlHelper.Deserialize<T>(tableXML, "Table");
                                if (response is T)
                                    return (T)response;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            return null;
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
