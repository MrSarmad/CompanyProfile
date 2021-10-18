using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public class PersonifyRequest
    {
        public string Name { get; set; }
        public Dictionary<string, string> ParametersList { get; set; }
    }

    public class PersonifyBaseParameter
    {
        [JsonProperty("usr_customer_number")]
        public int CompanyId { get; set; }

        [JsonProperty("user_id")]
        public string UserName { get; set; }
    }

    public class PersonifyRequestParameter : PersonifyBaseParameter
    {
        [JsonProperty("about_us")]
        public string AboutUsText { get; set; }
    }
}
