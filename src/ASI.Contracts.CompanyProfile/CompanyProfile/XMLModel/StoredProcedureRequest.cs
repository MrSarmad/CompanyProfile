using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel
{
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
}
