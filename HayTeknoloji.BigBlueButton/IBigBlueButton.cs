using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HayTeknoloji.BigBlueButton.Models;

namespace HayTeknoloji.BigBlueButton
{
    public interface IBigBlueButton
    {
        Task<ResultModel<Response>> GetVersion(string serverUrl = null, CancellationToken cancellationToken = default);

        Task<ResultModel<CreateMeetingResponse>> CreateMeetingAsync(string meetingId,
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
            CancellationToken cancellationToken = default
            // string meetingEndUrl = null,
            // string recordingEndUrl = null,
            // bool? record = null,
            // bool? allowStartStopRecording = null,
            // bool? autoStartRecording = null
        );


        Task<ResultModel<GetMeetingsResponse>> GetMeetings(string serverUrl = null, string salt = null,
            CancellationToken cancellationToken = default);

        Task<ResultModel<MeetingInfoResponse>> GetMeetingInfo(string meetingId, string serverUrl = null, string salt = null, string password = null,
            CancellationToken cancellationToken = default);

        Task<ResultModel<IsMeetingRunningResponse>> IsMeetingRunning(string meetingId, string serverUrl = null, string salt = null,
            CancellationToken cancellationToken = default);

        Task<ResultModel<Response>> EndMeetingAsync(string meetingId, string serverUrl = null, string salt = null, string password = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Join the meeting, user role depend on the password param
        /// </summary>
        /// <param name="meetingId">meeiting id</param>
        /// <param name="username">user's name</param>
        /// <param name="userId">user's Id</param>
        /// <param name="password">meeting password</param>
        /// <param name="serverUrl">Bbb server url. When serverUrl null, defaultSettings will be used</param>
        /// <param name="salt">Bbb server salt. When serverUrl null, defaultSettings will be used</param>
        /// <returns></returns>
        string GetMeetingJoinUrl(string meetingId, string username, string userId, string password, string serverUrl = null, string salt = null);
    }
}