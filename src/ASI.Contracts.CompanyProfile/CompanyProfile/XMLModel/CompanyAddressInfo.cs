using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel
{
    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(StoredProcedureOutput));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (StoredProcedureOutput)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "Table")]
    public class CompanyAddressInfo
    {

        [XmlElement(ElementName = "CUS_ADDRESS_ID")]
        public int CUSADDRESSID { get; set; }

        [XmlElement(ElementName = "ADDRESS_TYPE_CODE")]
        public string ADDRESSTYPECODE { get; set; }

        [XmlElement(ElementName = "ADDRESS_STATUS_CODE")]
        public string ADDRESSSTATUSCODE { get; set; }

        [XmlElement(ElementName = "ADDRESS_1")]
        public string ADDRESS1 { get; set; }

        [XmlElement(ElementName = "ADDRESS_2")]
        public object ADDRESS2 { get; set; }

        [XmlElement(ElementName = "ADDRESS_3")]
        public object ADDRESS3 { get; set; }

        [XmlElement(ElementName = "ADDRESS_4")]
        public object ADDRESS4 { get; set; }

        [XmlElement(ElementName = "CITY")]
        public string CITY { get; set; }

        [XmlElement(ElementName = "STATE")]
        public string STATE { get; set; }

        [XmlElement(ElementName = "POSTAL_CODE")]
        public string POSTALCODE { get; set; }

        [XmlElement(ElementName = "COUNTRY_CODE")]
        public string COUNTRYCODE { get; set; }

        [XmlElement(ElementName = "COUNTRY_DESCR")]
        public string COUNTRYDESCR { get; set; }

        [XmlElement(ElementName = "COUNTY")]
        public string COUNTY { get; set; }

        [XmlElement(ElementName = "PRIORITY_SEQ")]
        public int PRIORITYSEQ { get; set; }

        [XmlElement(ElementName = "BILL_TO_FLAG")]
        public string BILLTOFLAG { get; set; }

        [XmlElement(ElementName = "SHIP_TO_FLAG")]
        public string SHIPTOFLAG { get; set; }

        [XmlElement(ElementName = "ADDRESS_TYPE_CODE1")]
        public string ADDRESSTYPECODE1 { get; set; }
    }

    //[XmlRoot(ElementName = "NewDataSet")]
    //public class NewDataSet
    //{

    //    [XmlElement(ElementName = "Table")]
    //    public List<Table> Table { get; set; }
    //}

    //[XmlRoot(ElementName = "Data")]
    //public class Data
    //{

    //    [XmlElement(ElementName = "NewDataSet")]
    //    public NewDataSet NewDataSet { get; set; }
    //}

    //[XmlRoot(ElementName = "operationResult")]
    //public class OperationResult
    //{

    //    [XmlAttribute(AttributeName = "nil")]
    //    public bool Nil { get; set; }
    //}

    //[XmlRoot(ElementName = "StoredProcedureOutput")]
    //public class StoredProcedureOutput
    //{

    //    [XmlElement(ElementName = "Data")]
    //    public Data Data { get; set; }

    //    [XmlElement(ElementName = "operationResult")]
    //    public OperationResult OperationResult { get; set; }

    //    [XmlAttribute(AttributeName = "xsi")]
    //    public string Xsi { get; set; }

    //    [XmlAttribute(AttributeName = "xsd")]
    //    public string Xsd { get; set; }

    //    [XmlText]
    //    public string Text { get; set; }
    //}


}
