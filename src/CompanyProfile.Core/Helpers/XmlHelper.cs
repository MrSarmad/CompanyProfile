using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CompanyProfile.Core.Helpers
{
    public static class XmlHelper
    {
        public static string Serialize<TInput>(TInput objToSerialize) where TInput : class
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(TInput));
            var xml = string.Empty;
            using (var sww = new StringWriter())
            {
                using XmlWriter writer = XmlWriter.Create(sww);
                xsSubmit.Serialize(writer, objToSerialize);
                xml = sww.ToString();
            }
            return xml;
        }
    }
}
