using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;
using CompanyProfile.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CompanyProfile.Core.CompanyProfile
{
    public class PersonifyDataService
    {
        private readonly static string _psfyBase = "https://asi-prsw-02.uat-asicentral.com/PersonifyDataServices/PersonifyDataASI.svc/GetStoredProcedureDataXML";
        private readonly static string _psfyUser = "webuser";
        private readonly static string _psfyPass = "webuser2013";
        private readonly HttpClient _httpClient;

        public PersonifyDataService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            if (_httpClient != null)
            {
                //var baseRadarUri = ConfigurationManager.AppSettings["_psfyBase"];
                var baseRadarUri = _psfyBase;
                if (string.IsNullOrWhiteSpace(baseRadarUri))
                {
                    var exc = new Exception("invalid URL - missing RadarApiBase");
                    throw exc;
                }
                _httpClient = new HttpClient { BaseAddress = new Uri(baseRadarUri) };
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                                 Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_psfyUser}:{_psfyPass}")));
            }
        }

        public async Task<T> MakeRequest<T>(StoredProcedureRequest req) where T : class
        {
            using var httpClient = new HttpClient();//todo: see company insights on IOC implementation for this
            httpClient.BaseAddress = new Uri(_psfyBase);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                            Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_psfyUser}:{_psfyPass}")));

            //var xmlSvc = new XmlService();
            var serializedXml = XmlHelper.Serialize(req);
            var psfyPostResponse = await httpClient.PostAsync("", new StringContent(serializedXml, Encoding.UTF8, "text/xml"));
            var psfyResponseContent = await psfyPostResponse.Content.ReadAsStringAsync();

            try
            {
                //serialize to standard response object
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
    }
}
