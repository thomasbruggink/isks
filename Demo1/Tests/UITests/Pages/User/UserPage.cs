using UITests.Pages.User.Parts;

namespace UITests.Pages.User
{
    public class UserPage : IPage
    {
        private readonly UserRegisterPart _userRegister;
        private readonly UserLoginPart _userLogin;

        public UserPage()
        {
            _userRegister = new UserRegisterPart();
            _userLogin = new UserLoginPart();
        }

        public string GetUrl()
        {
            return "/User/Index";
        }

        public UserRegisterPart RegisterPart()
        {
            return _userRegister;
        }

        public UserLoginPart LoginPart()
        {
            return _userLogin;
        }
    }
}
