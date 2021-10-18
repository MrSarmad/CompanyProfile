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
        public string Business_Hours { get; set; }
        /// <remarks/>
        public int Year_Established { get; set; }
        /// <remarks/>
        public string Sales_Volume_for_12_months { get; set; }
        /// <remarks/>
        public string Advertising_Specialty_Sales_Volume { get; set; }
        /// <remarks/>
        public int? Number_Of_Employees { get; set; }
        /// <remarks/>        
        public string About_Us { get; set; }
        /// <remarks/>        
        public string Female_Owned { get; set; }
        /// <remarks/>
        public string Veteran_Owned { get; set; }
        /// <remarks/>
        public string Asian_Owned { get; set; }
        /// <remarks/>
        public string Hispanic_Owned { get; set; }
        /// <remarks/>
        public string African_American_Owned { get; set; }
        /// <remarks/>
        public string Native_American_Owned { get; set; }
        /// <remarks/>
        public string Jewish_Owned { get; set; }
        /// <remarks/>
        public string Disabled_Owned { get; set; }
        /// <remarks/>
        public string ESOP { get; set; }
        /// <remarks/>
        public string Cert_Available { get; set; }
        /// <remarks/>
        public string Small_Disadvantage { get; set; }
        /// <remarks/>
        public string LGBTQ_Owned { get; set; }
    }
}
