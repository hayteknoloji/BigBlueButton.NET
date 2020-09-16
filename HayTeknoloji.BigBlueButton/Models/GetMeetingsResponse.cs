using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HayTeknoloji.BigBlueButton.Models
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "response", IsNullable = false, Namespace = "")]
    public class GetMeetingsResponse : Response
    {
        [XmlArray("meetings")]
        [XmlArrayItem("meeting")]
        public List<MeetingInfoResponse> Meetings { get; set; }
    }
}