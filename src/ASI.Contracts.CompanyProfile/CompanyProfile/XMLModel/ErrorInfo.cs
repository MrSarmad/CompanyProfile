using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel
{
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "Table", Namespace = "", IsNullable = false)]
    public class ErrorInfo
    {
        [XmlElement(ElementName = "ErrorNumber")]
        public int ErrorNumber { get; set; }

        [XmlElement(ElementName = "ErrorSeverity")]
        public object ErrorSeverity { get; set; }

        [XmlElement(ElementName = "ErrorState")]
        public object ErrorState { get; set; }

        [XmlElement(ElementName = "ErrorProcedure")]
        public object ErrorProcedure { get; set; }

        [XmlElement(ElementName = "ErrorLine")]
        public object ErrorLine { get; set; }

        [XmlElement(ElementName = "ErrorMessage")]
        public object ErrorMessage { get; set; }
    }
}
