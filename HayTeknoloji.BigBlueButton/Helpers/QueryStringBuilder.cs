using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HayTeknoloji.BigBlueButton.Helpers
{
    public class QueryStringBuilder : Dictionary<string, object>
    {
        public override string ToString()
        {
            var array = this.Where(p => p.Value != null).Select(p =>
            {
                var key = WebUtility.UrlEncode(p.Key);
                var value = WebUtility.UrlEncode(p.Value.ToString());

                return key + "=" + value;
            }).ToArray();
            return string.Join("&", array);
        }
    }
}