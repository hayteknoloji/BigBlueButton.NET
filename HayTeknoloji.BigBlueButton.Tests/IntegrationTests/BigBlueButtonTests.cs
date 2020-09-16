using System;
using System.Net.Http;
using FluentAssertions;
using HayTeknoloji.BigBlueButton.Helpers;
using HayTeknoloji.BigBlueButton.Models;
using HayTeknoloji.BigBlueButton.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace HayTeknoloji.BigBlueButton.Tests.IntegrationTests
{
    public class BigBlueButtonTests
    {
        private readonly IBigBlueButton _bbb;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Settings _defaultSettings;

        public BigBlueButtonTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            _defaultSettings = new Settings
            {
                ServerUrl = Environment.GetEnvironmentVariable("BbbServerUrl"),
                Salt = Environment.GetEnvironmentVariable("BbbServerSalt"),
                AttendeePassword = Environment.GetEnvironmentVariable("BbbServerAttendeePassword"),
                ModeratorPassword = Environment.GetEnvironmentVariable("BbbServerModeratorPassword"),
                WelcomeMessage = "Bismillah",
                LogoutUrl = "http://hayteknoloji.com",
                GuestPolicy = "ALWAYS_ACCEPT",
                MaxParticipants = 10,
                Duration = 2
            };
            _bbb = new BigBlueButton(new DefaultHttpClientFactory(), _defaultSettings);
        }

        [Fact]
        public async void GetApiVersion_Test()
        {
            /* Arrange */

            /* Act */
            var response = await _bbb.GetVersion();

            /* Assert */
            response.IsSuccess.Should().Be(true);
            response.BbbResponse.Version.Should().Be("2.0");
        }

        [Fact]
        public async void CreateMeeting_Test()
        {
            /* Arrange */

            /* Act */
            var response = await _bbb.CreateMeetingAsync("Test", "test", duration: 1);

            /* Assert */
            response.IsSuccess.Should().Be(true);
            response.BbbResponse.MeetingId.Should().Be("Test");
        }

        [Fact]
        public async void IsMeetingRunning_Test()
        {
            /* Arrange */
            var createResponse = await _bbb.CreateMeetingAsync("IsMeetingRunning_Test", "test");
            createResponse.IsSuccess.Should().Be(true);

            /* Act */
            var response = await _bbb.IsMeetingRunning("IsMeetingRunning_Test");

            /* Assert */
            response.IsSuccess.Should().Be(true);
            response.BbbResponse.Running.Should().BeFalse();
        }

        [Fact]
        public async void GetMeetingInfo_Test()
        {
            /* Arrange */
            var createResponse = await _bbb.CreateMeetingAsync("GetMeetingInfo_Test", "test");
            createResponse.IsSuccess.Should().Be(true);

            /* Act */
            var response = await _bbb.GetMeetingInfo("GetMeetingInfo_Test");

            /* Assert */
            response.IsSuccess.Should().Be(true);
            response.BbbResponse.MeetingId.Should().Be("GetMeetingInfo_Test");
        }

        [Fact]
        public async void GetMeetings_Test()
        {
            /* Arrange */
            var createResponse = await _bbb.CreateMeetingAsync("GetMeetings_Test1", "test");
            createResponse.IsSuccess.Should().Be(true);
            createResponse = await _bbb.CreateMeetingAsync("GetMeetings_Test2", "test");
            createResponse.IsSuccess.Should().Be(true);

            /* Act */
            var response = await _bbb.GetMeetings();

            /* Assert */
            response.IsSuccess.Should().Be(true);
            response.BbbResponse.Meetings.Should().Contain(infoResponse => infoResponse.MeetingId == "GetMeetings_Test1");
            response.BbbResponse.Meetings.Should().Contain(infoResponse => infoResponse.MeetingId == "GetMeetings_Test2");
        }

        [Fact]
        public async void EndMeeting_Test()
        {
            /* Arrange */
            var createResponse = await _bbb.CreateMeetingAsync("EndMeeting_Test", "test");
            createResponse.IsSuccess.Should().Be(true);

            /* Act */
            var response = await _bbb.EndMeetingAsync("EndMeeting_Test");

            /* Assert */
            response.IsSuccess.Should().Be(true);
            response.BbbResponse.MessageKey.Should().Be("sentEndMeetingRequest");
        }

        [Fact]
        public async void GetMeetingJoinUrl_Test()
        {
            /* Arrange */
            var checkSum = UrlHelper.CheckSum("join" + "fullName=test-user&meetingID=GetMeetingJoinUrl_Test&userID=test-user-id&password=" +
                                              _defaultSettings.ModeratorPassword + _defaultSettings.Salt);
            var joinUrl =
                $"{_defaultSettings.ServerUrl}bigbluebutton/api/join?checksum={checkSum}&fullName=test-user&meetingID=GetMeetingJoinUrl_Test&userID=test-user-id&password={_defaultSettings.ModeratorPassword}";


            /* Act */
            var url = _bbb.GetMeetingJoinUrl("GetMeetingJoinUrl_Test", "test-user", "test-user-id", _defaultSettings.ModeratorPassword);

            /* Assert */

            url.Should().Be(joinUrl);
        }
    }
}