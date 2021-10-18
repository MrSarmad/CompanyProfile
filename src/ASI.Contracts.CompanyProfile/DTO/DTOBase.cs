using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ASI.Contracts.CompanyProfile.CompanyProfile.DTO
{
    public class DTOBase
    {
        public string CompanyId {get; set;}
        public string UserId { get; set; } = "CURWILER";
        public string UserName { get; set; }
    }
}
