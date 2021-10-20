using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CompanyProfile.Personify.Helpers
{
    public static class XmlHelper
    {
        public static string Serialize<TInput>(TInput objToSerialize, string root = null) where TInput : class
        {
            var xml = string.Empty;
            XmlSerializer xmlSerializer = null;

            if (string.IsNullOrEmpty(root))
                xmlSerializer = new XmlSerializer(typeof(TInput));
            else
                xmlSerializer = new XmlSerializer(typeof(TInput), new XmlRootAttribute(root));
            
            using (var sww = new StringWriter())
            {
                using XmlWriter writer = XmlWriter.Create(sww);
                xmlSerializer.Serialize(writer, objToSerialize);
                xml = sww.ToString();
            }
            return xml;
        }

        public static object Deserialize<TInput>(string xmlString, string root = null) where TInput : class
        {
            object returnVal = null;
            XmlSerializer xmlSerializer = null;

            if (string.IsNullOrEmpty(root))
                xmlSerializer = new XmlSerializer(typeof(TInput));
            else
                xmlSerializer = new XmlSerializer(typeof(TInput), new XmlRootAttribute(root));

            using (TextReader reader = new StringReader(xmlString))
            {
                object output = xmlSerializer.Deserialize(reader);
                if (output != null && (TInput)output != null)
                {
                    returnVal = (TInput)output;
                }
            }

            return returnVal;
        }

        public static string ConvertToXML(string xmlString)
        {
            var xmlDocument = new XmlDocument();
            var node = string.Empty;
            try
            {
                xmlDocument.LoadXml(xmlString);
                using (var writer = new StringWriter())
                {
                    xmlDocument.Save(writer);
                    node = writer.ToString();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return node;
        }
    }
}
