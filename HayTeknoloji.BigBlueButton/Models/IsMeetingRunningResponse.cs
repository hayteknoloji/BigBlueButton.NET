using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HayTeknoloji.BigBlueButton.Models
{
    
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "response", IsNullable = false, Namespace = "")]
    public class IsMeetingRunningResponse: Response
    {
        [XmlElement("running")]
        public bool Running { get; set; }
    }
}