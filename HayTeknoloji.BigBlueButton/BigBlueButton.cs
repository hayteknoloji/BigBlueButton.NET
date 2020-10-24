using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HayTeknoloji.BigBlueButton.Helpers;
using HayTeknoloji.BigBlueButton.Models;

namespace HayTeknoloji.BigBlueButton
{
    public class BigBlueButton : IBigBlueButton
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Settings _defaultSettings;

        public BigBlueButton(IHttpClientFactory clientFactory, Settings defaultSettings)
        {
            _clientFactory = clientFactory;
            _defaultSettings = defaultSettings;
        }

        public async Task<ResultModel<Response>> GetVersion(string serverUrl = null, CancellationToken cancellationToken = default)
        {
            var baseUri = new Uri(serverUrl ?? _defaultSettings.ServerUrl);
            var url = new Uri(baseUri, "bigbluebutton/api").ToString();

            return await RequestHelper.MakeRequest<Response>(_clientFactory.CreateClient(), url, null, cancellationToken);
        }

        public async Task<ResultModel<CreateMeetingResponse>> CreateMeetingAsync(
            string meetingId,
            string name,
            string serverUrl = null,
            string salt = null,
            string attendeePassword = null,
            string moderatorPassword = null,
            string welcome = null,
            string logoutUrl = null,
            string guestPolicy = null,
            int? maxParticipants = null,
            int? duration = null,
            bool? lockSettingsDisableCam = null,
            bool? lockSettingsDisableMic = null,
            bool? lockSettingsDisablePrivateChat = null,
            bool? lockSettingsDisablePublicChat = null,
            bool? lockSettingsLockOnJoinConfigurable = null,
            bool? webcamsOnlyForModerator = null,
            bool? allowModsToUnmuteUsers = null,
            // string meetingEndUrl = null,
            // string recordingEndUrl = null,
            // bool? record = null,
            // bool? allowStartStopRecording = null,
            // bool? autoStartRecording = null
            CancellationToken cancellationToken = default
        )
        {
            var qb = new QueryStringBuilder
            {
                {"meetingID", meetingId},
                {"name", name},
                {"attendeePW", attendeePassword ?? _defaultSettings.AttendeePassword},
                {"moderatorPW", moderatorPassword ?? _defaultSettings.ModeratorPassword},

                {"welcome", welcome ?? _defaultSettings.WelcomeMessage},
                {"logoutURL", logoutUrl ?? _defaultSettings.LogoutUrl},

                {"maxParticipants", maxParticipants ?? _defaultSettings.MaxParticipants},
                {"duration", duration ?? _defaultSettings.Duration},
                {"guestPolicy", guestPolicy ?? _defaultSettings.GuestPolicy},

                {"lockSettingsDisableCam", lockSettingsDisableCam ?? _defaultSettings.LockSettingsDisableCam},
                {"lockSettingsDisableMic", lockSettingsDisableMic ?? _defaultSettings.LockSettingsDisableMic},
                {"lockSettingsDisablePrivateChat", lockSettingsDisablePrivateChat ?? _defaultSettings.LockSettingsDisablePrivateChat},
                {"lockSettingsDisablePublicChat", lockSettingsDisablePublicChat ?? _defaultSettings.LockSettingsDisablePublicChat},
                {"lockSettingsLockOnJoinConfigurable", lockSettingsLockOnJoinConfigurable ?? _defaultSettings.LockSettingsLockOnJoinConfigurable},

                {"webcamsOnlyForModerator", webcamsOnlyForModerator ?? _defaultSettings.WebcamsOnlyForModerator},
                {"allowModsToUnmuteUsers", allowModsToUnmuteUsers ?? _defaultSettings.AllowModsToUnmuteUsers},

                // {"recordingmarks", "true"},
                // {"record", record?.ToString() ?? _defaultSettings.Record.ToString()},
                // {"allowStartStopRecording", allowStartStopRecording.ToString()},
                // {"autoStartRecording", autoStartRecording.ToString()},
                // {"meta_endCallbackUrl", (meetingEndUrl)},
                // {"meta_bbb-recording-ready-url", (recordingEndUrl)},
            };
            var url = UrlHelper.GetBbbUri(serverUrl ?? _defaultSettings.ServerUrl, salt ?? _defaultSettings.Salt, "create", qb.ToString());
            var result = await RequestHelper.MakeRequest<CreateMeetingResponse>(_clientFactory.CreateClient(), url, meetingId, cancellationToken);

            return result;
        }

        public async Task<ResultModel<IsMeetingRunningResponse>> IsMeetingRunning(string meetingId, string serverUrl = null, string salt = null,
            CancellationToken cancellationToken = default)
        {
            var qb = new QueryStringBuilder
            {
                {"meetingID", meetingId}
            };
            var url = UrlHelper.GetBbbUri(serverUrl ?? _defaultSettings.ServerUrl, salt ?? _defaultSettings.Salt, "isMeetingRunning", qb.ToString());
            var result = await RequestHelper.MakeRequest<IsMeetingRunningResponse>(_clientFactory.CreateClient(), url, meetingId, cancellationToken);

            return result;
        }

        public async Task<ResultModel<MeetingInfoResponse>> GetMeetingInfo(string meetingId, string serverUrl = null, string salt = null,
            string password = null,
            CancellationToken cancellationToken = default)
        {
            var qb = new QueryStringBuilder
            {
                {"meetingID", meetingId},
                {"password", password ?? _defaultSettings.ModeratorPassword},
            };

            var url = UrlHelper.GetBbbUri(serverUrl ?? _defaultSettings.ServerUrl, salt ?? _defaultSettings.Salt, "getMeetingInfo", qb.ToString());
            var result = await RequestHelper.MakeRequest<MeetingInfoResponse>(_clientFactory.CreateClient(), url, meetingId, cancellationToken);

            return result;
        }


        public async Task<ResultModel<GetMeetingsResponse>> GetMeetings(string serverUrl = null, string salt = null,
            CancellationToken cancellationToken = default)
        {
            var url = UrlHelper.GetBbbUri(serverUrl ?? _defaultSettings.ServerUrl, salt ?? _defaultSettings.Salt, "getMeetings", "");
            var result = await RequestHelper.MakeRequest<GetMeetingsResponse>(_clientFactory.CreateClient(), url, null, cancellationToken);

            return result;
        }

        public async Task<ResultModel<Response>> EndMeetingAsync(string meetingId, string serverUrl = null, string salt = null,
            string password = null, CancellationToken cancellationToken = default)
        {
            var qb = new QueryStringBuilder
            {
                {"meetingID", meetingId},
                {"password", password ?? _defaultSettings.ModeratorPassword},
            };

            var url = UrlHelper.GetBbbUri(serverUrl ?? _defaultSettings.ServerUrl, salt ?? _defaultSettings.Salt, "end", qb.ToString());
            var result = await RequestHelper.MakeRequest<Response>(_clientFactory.CreateClient(), url, meetingId, cancellationToken);

            return result;
        }


        public string GetMeetingJoinUrl(string meetingId, string username, string userId, string password, string serverUrl = null,
            string salt = null)
        {
            var qb = new QueryStringBuilder
            {
                {"fullName", username},
                {"meetingID", meetingId},
                {"userID", userId},
                {"password", password}
            };
            return UrlHelper.GetBbbUri(serverUrl ?? _defaultSettings.ServerUrl, salt ?? _defaultSettings.Salt, "join", qb.ToString());
        }
    }
}