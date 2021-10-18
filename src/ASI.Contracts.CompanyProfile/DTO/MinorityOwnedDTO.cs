using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ASI.Contracts.CompanyProfile.CompanyProfile.DTO
{
    public class MinorityOwnedDTO : DTOBase
    {    
        public char FemaleOwned { get; set; }
        public char VeteranOwned { get; set; }
        public char AsianOwned { get; set; }
        public char HispanicOwned { get; set; }
        public char AfricanAmericanOwned { get; set; }
        public char NativeAmericanOwned { get; set; }
        public char JewishOwned { get; set; }
        public char DisabledOwned { get; set; }
        public char Esop { get; set; }
        public char CertAvailable { get; set; }
        public char SmallDisadvantage { get; set; }
        public char LgbtqOwned { get; set; }        
    }
}
