using WXTechChallenge.Common.Dtos.Response;
using WXTechChallenge.Common.Services.Interfaces;
using WXTechChallenge.Common.Settings;

namespace WXTechChallenge.Common.Services
{
    public class UserService : IUserService
    {
        private readonly UserSettings _userSettings;
        public UserService(UserSettings userSettings)
        {
            _userSettings = userSettings;
        }
        public UserDto GetUser()
        {
            return new UserDto { Name = _userSettings.Name, Token = _userSettings.Token };
        }
    }
}
