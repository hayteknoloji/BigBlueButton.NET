using System.IO;
using System.Xml.Serialization;

namespace HayTeknoloji.BigBlueButton.Helpers
{
    public static class XmlHelper
    {
        public static T Deserialize<T>(this Stream input) where T : class
        {
            var seriazlizer = new XmlSerializer(typeof(T));
            return (T) seriazlizer.Deserialize(input);
        }

        public static string Serialize<T>(this T obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }
    }
}