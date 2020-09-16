using System;
using System.ComponentModel;
using System.Xml.Serialization;
using HayTeknoloji.BigBlueButton.Helpers;

namespace HayTeknoloji.BigBlueButton.Models
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "response", IsNullable = false, Namespace = "")]
    public class Response
    {
        [XmlElement("returncode")] 
        public string ReturnCode { get; set; }
        
        [XmlElement("version")] 
        public string Version { get; set; }
        
        [XmlElement("messageKey")] 
        public string MessageKey { get; set; }
        
        [XmlElement("message")] 
        public string Message { get; set; }

        /// <summary>
        ///     Convert xml to object
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Parse<T>(string xml) where T : Response
        {
            return null; //xml.Deserialize<T>();
        }
    }
}