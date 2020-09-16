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
    public class MeetingInfoResponse : Response
    {
        [XmlElement("meetingID")] 
        public string MeetingId { get; set; }

        [XmlElement("internalMeetingID")] 
        public string InternalMeetingId { get; set; }

        [XmlElement("meetingName")] 
        public string MeetingName { get; set; }
        
        [XmlElement("createTime")] 
        public long CreateTime { get; set; }
        
        [XmlElement("startTime")] 
        public long StartTime { get; set; }
        
        [XmlElement("endTime")] 
        public long EndTime { get; set; }
        
        [XmlElement("voiceBridge")] 
        public int VoiceBridge { get; set; }
        
        [XmlElement("participantCount")] 
        public int ParticipantCount { get; set; }

        [XmlElement("dialNumber")] 
        public string DialNumber { get; set; }

        [XmlElement("attendeePW")] 
        public string AttendeePw { get; set; }

        [XmlElement("moderatorPW")] 
        public string ModeratorPw { get; set; }

        [XmlElement("running")] 
        public bool Running { get; set; }

        [XmlElement("hasBeenForciblyEnded")] 
        public bool HasBeenForciblyEnded { get; set; }

        [XmlElement("hasUserJoined")] 
        public bool HasUserJoined { get; set; }

        [XmlElement("recording")] 
        public bool Recording { get; set; }

        [XmlElement("listenerCount")] 
        public int ListenerCount { get; set; }

        [XmlElement("voiceParticipantCount")] 
        public int VoiceParticipantCount { get; set; }

        [XmlElement("videoCount")] 
        public int VideoCount { get; set; }

        [XmlElement("maxUsers")] 
        public int MaxUsers { get; set; }

        [XmlElement("moderatorCount")] 
        public int ModeratorCount { get; set; }

        [XmlArray("attendees")]
        [XmlArrayItem("attendee")]
        public List<AttendeeModel> Attendees { get; set; }
    }
}