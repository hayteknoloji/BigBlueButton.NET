namespace HayTeknoloji.BigBlueButton.Models
{
    public class Settings
    {
        public string ServerUrl { get; set; }
        public string Salt { get; set; }
        public string ModeratorPassword { get; set; }
        public string AttendeePassword { get; set; }
        public string WelcomeMessage { get; set; }
        public string LogoutUrl { get; set; }
        public string GuestPolicy { get; set; } = "ALWAYS_ACCEPT";
        public int MaxParticipants { get; set; }
        public int Duration { get; set; }

        public bool LockSettingsDisableCam { get; set; }
        public bool LockSettingsDisableMic { get; set; }
        public bool LockSettingsDisablePrivateChat { get; set; }
        public bool LockSettingsDisablePublicChat { get; set; }
        public bool LockSettingsLockOnJoinConfigurable { get; set; }
        public bool WebcamsOnlyForModerator { get; set; }
        public bool AllowModsToUnmuteUsers { get; set; }
    }
}