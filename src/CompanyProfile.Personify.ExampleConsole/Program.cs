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
 
namespace CompanyProfile.Personify.ExampleConsole
{
    /// <summary>
    /// a simple program to test out ideas for how to get PSFY data using some generic components.
    /// </summary>
    internal class Program
    {
        private readonly static string _psfyBase = "https://asi-prsw-02.uat-asicentral.com/PersonifyDataServices/PersonifyDataASI.svc/GetStoredProcedureDataXML";
        private readonly static string _psfyUser = "webuser";
        private readonly static string _psfyPass = "webuser2013";

        private static async Task Main(string[] args)
        {
            var asiNum = "33020";

            var result = await GetGeneralInfo(asiNum);
            DisplayObject(result); 

            var addys = await GetAddressInfo(asiNum);
            DisplayObject(addys);

            //var phones = await GetPhoneInfo(asiNum);
            Console.ReadLine(); 
        }

        private static void DisplayObject<T>(T obj) where T: class
        {
            var serializedResult = JsonConvert.SerializeObject(obj);
            Console.WriteLine(serializedResult);
        }

        private static async Task<CompanyGeneralInfo> GetGeneralInfo(string asiNumber)
        {
            var procName = "USR_CPI_General_Select";
            var procParams = new Dictionary<string, string>();
            var currentUserName = GetCurrentUserName();

            procParams.Add("@ip_usr_customer_number", "9207"); //.PadLeft(12, '0'));//why is this padded with zeros? 
            //procParams.Add("@ip_sub_customer_id", "0"); //what does this mean? 
            procParams.Add("@ip_user_id", currentUserName);

            var psfyRequest = GetProcRequest(procName, procParams);

            var results = await MakeRequest<PsfyGeneralInfoContainer>(psfyRequest);

            return results.Table;
        }

        private static string GetCurrentUserName()
        {
            return "CURWILER"; //todo: how should we get this? Need to ask PSFY team
        }

        private static async Task<object> GetAddressInfo(string asiNumber)
        {
            var procName = "USR_CPI_Address_Select";
            var procParams = new Dictionary<string, string>();
            var currentUserName = GetCurrentUserName();

            procParams.Add("@ip_master_customer_id", asiNumber.PadLeft(12, '0'));//why is this padded with zeros? 
            procParams.Add("@ip_sub_customer_id", "0"); //what does this mean? 
            procParams.Add("@ip_user_id", currentUserName);

            var psfyRequest = GetProcRequest(procName, procParams);

            var results = await MakeRequest<PsfyGeneralInfoContainer>(psfyRequest);

            //here, convert results to some DTO


            return results;

        }

        private static StoredProcedureRequest GetProcRequest(string procName, Dictionary<string, string> procParams)
        {
            var req = new StoredProcedureRequest
            {
                StoredProcedureName = procName,
                IsUDFScalar = true,
                IsUserDefinedFunction = false,
                SPParameterList = new StoredProcedureRequestParameter[] { }
            };

            var paramList = new List<StoredProcedureRequestParameter>();

            foreach (var kvp in procParams)
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

        private static async Task<T> MakeRequest<T>(StoredProcedureRequest req) where T : class
        {
            using var httpClient = new HttpClient();//todo: see company insights on IOC implementation for this
            httpClient.BaseAddress = new Uri(_psfyBase);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                            Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_psfyUser}:{_psfyPass}")));

            var xmlSvc = new XmlService();
            var serializedXml = xmlSvc.Serialize(req);

            var psfyPostResponse = await httpClient.PostAsync("", new StringContent(serializedXml, Encoding.UTF8, "text/xml"));

            var psfyResponseContent = await psfyPostResponse.Content.ReadAsStringAsync();

            //serialize to standard result object
            StoredProcedureOutput output;
            var xmlSerializer = new XmlSerializer(typeof(StoredProcedureOutput), new XmlRootAttribute("StoredProcedureOutput"));
            using (TextReader reader = new StringReader(psfyResponseContent))
            {
                output = (StoredProcedureOutput)xmlSerializer.Deserialize(reader);
            }
            try
            {
                var xmlDoc = new XmlDocument();
                var dataNode = string.Empty;
                xmlDoc.LoadXml(output.Data);
                using (var writer = new StringWriter())
                {
                    xmlDoc.Save(writer);

                    dataNode = writer.ToString();
                }

                if (!string.IsNullOrWhiteSpace(dataNode))
                {
                    //deserialize the datanode into provided type
                    using TextReader reader = new StringReader(dataNode);
                    var dataNodeSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute("NewDataSet"));
                    var datanodeObj = dataNodeSerializer.Deserialize(reader);
                    if (datanodeObj is T)
                        return (T)datanodeObj;
                }
            }
            catch (Exception ex)
            {
                //log the exc
                DisplayObject(ex);
                
                return null;
            }
            return null;
        }
    }
    public class XmlService
    {
        public string Serialize<TInput>(TInput objToSerialize) where TInput : class
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(TInput));
            var xml = string.Empty;
            using (var sww = new StringWriter())
            {
                using XmlWriter writer = XmlWriter.Create(sww);
                xsSubmit.Serialize(writer, objToSerialize);
                xml = sww.ToString();
            }
            return xml;
        }

        //public TOut Deserialize<TOut>(string inputXml) where TOut : class
        //{
        //	var x = "blah"; 
        //	return (TOut)x; 
        //}
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class StoredProcedureOutput
    {
        /// <remarks/>
        public string Data { get; set; }
        /// <remarks/>
        [XmlElement(IsNullable = true)]
        public object operationResult { get; set; }
    }
    
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class StoredProcedureRequest
    {
        /// <remarks/>
        public string StoredProcedureName { get; set; }

        /// <remarks/>
        public bool IsUserDefinedFunction { get; set; }

        /// <remarks/>
        public bool IsUDFScalar { get; set; }

        /// <remarks/>
        [XmlArrayItem("StoredProcedureParameter", IsNullable = false)]
        public StoredProcedureRequestParameter[] SPParameterList { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class StoredProcedureRequestParameter
    {
        /// <remarks/>
        public string Name { get; set; }

        /// <remarks/>
        public string Value { get; set; }

        /// <remarks/>
        public byte Direction { get; set; }
    }


    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "NewDataSet", Namespace = "", IsNullable = false)]
    public partial class PsfyGeneralInfoContainer
    {
        /// <remarks/>
        public CompanyGeneralInfo Table { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "Table", Namespace = "", IsNullable = false)]
    public partial class CompanyGeneralInfo
    {
        public object Business_Hours { get; set; }
        /// <remarks/>
        public object Year_Established { get; set; }
        /// <remarks/>
        public object Sales_Volume_for_12_months { get; set; }
        /// <remarks/>
        public object Advertising_Specialty_Sales_Volume { get; set; }
        /// <remarks/>
        public string Number_Of_Employees { get; set; }
        /// <remarks/>
        public object About_Us { get; set; }
        /// <remarks/>
        public object Female_Owned { get; set; }
        /// <remarks/>
        public object Veteran_Owned { get; set; }
        /// <remarks/>
        public object Asian_Owned { get; set; }
        /// <remarks/>
        public object Hispanic_Owned { get; set; }
        /// <remarks/>
        public object African_American_Owned { get; set; }
        /// <remarks/>
        public object Native_American_Owned { get; set; }
        /// <remarks/>
        public object Jewish_Owned { get; set; }
        /// <remarks/>
        public object Disabled_Owned { get; set; }
        /// <remarks/>
        public object ESOP { get; set; }
        /// <remarks/>
        public object Cert_Available { get; set; }
        /// <remarks/>
        public object Small_Disadvantage { get; set; }
        /// <remarks/>
        public object LGBTQ_Owned { get; set; }
    }


}
