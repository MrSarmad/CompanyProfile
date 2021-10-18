using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel
{
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "NewDataSet", Namespace = "", IsNullable = false)]
    public partial class StoredProcedureResponseContainer
    {
        /// <remarks/>
        [XmlElement(ElementName = "Table", Namespace = "")]
        public object Table { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class StoredProcedureResponse
    {
        [XmlElement("Data")]
        public string Data { get; set; }
        /// <remarks/>
        [XmlElement(IsNullable = true)]
        public object operationResult { get; set; }
    }
}
