using System.Threading.Tasks;
using Actions.Dashboard;
using CatCore.Models.Twitch.Helix.Responses;

namespace Actions.Twitch
{
    internal class TwitchActionUser : IActionUser
    {
        private readonly ISocialPlatform _socialPlatform;

        public string ID { get; }
        public string Name { get; }
        public string? ProfilePictureURL { get; }

        public TwitchActionUser(ISocialPlatform socialPlatform, UserData user)
        {
            ID = user.UserId;
            Name = user.DisplayName;
            ProfilePictureURL = user.ProfileImageUrl;
            _socialPlatform = socialPlatform;
        }

        public Task Ban(uint? length)
        {
            if (_socialPlatform is TwitchSocialPlatform twitchPlatform)
            {
                twitchPlatform.InteractWithHelix((helix, selectedChannelId) => helix.BanUser(selectedChannelId, ID, length));
            }

            return Task.CompletedTask;
        }
    }
}