using WXTechChallenge.Dtos.Response;
using WXTechChallenge.Services.Interfaces;
using WXTechChallenge.Settings;

namespace WXTechChallenge.Services
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
            return new UserDto() { Name = _userSettings.Name, Token = _userSettings.Token };
        }
    }
}
