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
        public string About_Us { get; set; }
        /// <remarks/>        
        public string Female_Owned { get; set; }
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
