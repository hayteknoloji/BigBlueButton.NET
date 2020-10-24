using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HayTeknoloji.BigBlueButton.Models;
using Microsoft.Extensions.Logging;

namespace HayTeknoloji.BigBlueButton.Helpers
{
    public static class RequestHelper
    {
        public static async Task<ResultModel<T>> MakeRequest<T>(HttpClient client, string uri, string meetingId, CancellationToken stoppingToken)
            where T : class
        {
            var result = new ResultModel<T>() {MeetingId = meetingId};
            try
            {
                var response = await client.GetAsync(uri, stoppingToken);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    result.IsSuccess = true;
                    result.BbbResponse = stream.Deserialize<T>();
                    return result;
                }

                result.Exception = new Exception(await response.Content.ReadAsStringAsync());
                return result;
            }
            catch (Exception e)
            {
                result.InternalError = true;
                result.Exception = e;
                return result;
            }
        }

        public static async Task<T> MakePostRequest<T>(HttpClient client, string uri, string body, ILogger logger, CancellationToken stoppingToken,
            string logprefix = "")
            where T : class
        {
            logger.LogInformation($"Request start: {uri}");
            try
            {
                var httpContent = new StringContent(body, Encoding.UTF8, "application/xml");
                var response = await client.PostAsync(uri, httpContent, stoppingToken);
                if (response.IsSuccessStatusCode)
                {
                    var seriazlizer = new XmlSerializer(typeof(T), new XmlRootAttribute("response"));
                    var result = (T) seriazlizer.Deserialize(await response.Content.ReadAsStreamAsync());

                    logger.LogInformation($"Request succeed: {uri}");
                    return result;
                }

                logger.LogError($"{logprefix} BBB success dönmedi. {uri} - ", await response.Content.ReadAsStringAsync());
                return null;
            }
            catch (Exception e)
            {
                logger.LogError($"{logprefix} BBB istek sırasında hata oldu. {uri} - ", e.Message);
                return null;
            }
        }
    }
}