using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.CompanyProfile
{
    public class ProceduresInfo
    {
        public class USR_CPI_General_Select
        {
            public const string SPNAME = "USR_CPI_General_Select";
            public const string ip_usr_customer_number = "@ip_usr_customer_number";
            public const string ip_user_id = "@ip_user_id";
        }

        public class USR_CPI_General_Update_About_Us
        {
            public const string SPNAME = "USR_CPI_General_Update_About_Us ";
            public const string ip_usr_customer_number = "@ip_usr_customer_number";
            public const string ip_about_us = "@ip_about_us";
            public const string ip_user_id = "@ip_user_id";
        }

        public class USR_CPI_General_Update_Business_Hours
        {
            public const string SPNAME = "USR_CPI_General_Update_Business_Hours ";
            public const string ip_usr_customer_number = "@ip_usr_customer_number";
            public const string ip_business_hours = "@ip_business_hours";
            public const string ip_user_id = "@ip_user_id";
        }

        public class USR_CPI_General_Update_Minority_Owned
        {
            public const string SPNAME = "USR_CPI_General_Update_Minority_Owned ";
            public const string ip_usr_customer_number = "@ip_usr_customer_number";
            public const string ip_female_owned = "@ip_female_owned";
            public const string ip_veteran_owned = "@ip_veteran_owned";
            public const string ip_asian_owned = "@ip_asian_owned";
            public const string ip_hispanic_owned = "@ip_hispanic_owned";
            public const string ip_african_american_owned = "@ip_african_american_owned";
            public const string ip_native_american_owned = "@ip_native_american_owned";
            public const string ip_jewish_owned = "@ip_jewish_owned";
            public const string ip_disabled_owned = "@ip_disabled_owned";
            public const string ip_esop = "@ip_esop";
            public const string ip_cert_available = "@ip_cert_available";
            public const string ip_small_disadvantage = "@ip_small_disadvantage";
            public const string ip_lgbtq_owned = "@ip_lgbtq_owned";
            public const string ip_user_id = "@ip_user_id";
        }

        public class USR_CPI_General_Update_Number_Of_Employees
        {
            public const string SPNAME = "USR_CPI_General_Update_Number_Of_Employees ";
            public const string ip_usr_customer_number = "@ip_usr_customer_number";
            public const string ip_Number_Of_Employees = "@ip_Number_Of_Employees";
            public const string ip_user_id = "@ip_user_id";
        }

        public class USR_CPI_General_Update_Year_Established
        {
            public const string SPNAME = "USR_CPI_General_Update_Year_Established ";
            public const string ip_usr_customer_number = "@ip_usr_customer_number";
            public const string ip_year_established = "@ip_year_established";
            public const string ip_user_id = "@ip_user_id";
        }
    }
}
