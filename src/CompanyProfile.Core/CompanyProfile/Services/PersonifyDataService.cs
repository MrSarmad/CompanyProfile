using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> MakeRequest(StoredProcedureRequest req)
        {
            try
            {
                var serializedXml = Helpers.XmlHelper.Serialize(req);
                var psfyPostResponse = await _httpClient.PostAsync("", new StringContent(serializedXml, Encoding.UTF8, "text/xml"));
                var psfyResponseContent = await psfyPostResponse.Content.ReadAsStringAsync();
                return psfyResponseContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return string.Empty;
            //serialize to standard result object
            //StoredProcedureResponse output;
            //var xmlSerializer = new XmlSerializer(typeof(StoredProcedureResponse), new XmlRootAttribute("StoredProcedureOutput"));
            //using (TextReader reader = new StringReader(psfyResponseContent))
            //{
            //    output = (StoredProcedureResponse)xmlSerializer.Deserialize(reader);
            //}
            //try
            //{
            //    var xmlDoc = new XmlDocument();
            //    var dataNode = string.Empty;
            //    xmlDoc.LoadXml(output.Data);
            //    using (var writer = new StringWriter())
            //    {
            //        xmlDoc.Save(writer);

            //        dataNode = writer.ToString();
            //    }

            //    if (!string.IsNullOrWhiteSpace(dataNode))
            //    {
            //        //deserialize the datanode into provided type
            //        using TextReader reader = new StringReader(dataNode);
            //        var dataNodeSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute("NewDataSet"));
            //        var datanodeObj = dataNodeSerializer.Deserialize(reader);
            //        if (datanodeObj is T)
            //            return (T)datanodeObj;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //log the exc
            //    //DisplayObject(ex);

            //    return null;
            //}
            //return null;
        }
    }
}
