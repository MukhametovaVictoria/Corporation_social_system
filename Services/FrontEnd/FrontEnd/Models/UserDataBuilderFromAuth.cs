using FrontEnd.Services;

namespace FrontEnd.Models
{
    public class UserDataBuilderFromAuth : UserDataBuilder
    {
        private readonly AuthService _authService;
        private readonly string _userName;
        UserDataModel userModel = new UserDataModel();
        public UserDataBuilderFromAuth(AuthService authService, string userName)
        {
            _authService = authService;
            _userName = userName;
        }

        public override async Task BuildFromUserModel()
        {
            try
            {
                var user = await _authService.GetUserByLogin(_userName);
                if (user != null)
                {
                    userModel.Id = user.Id.ToString();
                    userModel.FullName = user.Name;
                }
            }
            catch (Exception ex) { }
        }

        public override UserDataModel GetResult()
        {
            return userModel;
        }

        public override void SetAdditional()
        {
            
        }

        public override void SetLanguage()
        {
            if (String.IsNullOrEmpty(userModel.Language))
                userModel.Language = Constants.LanguageBase;
        }

        public override void SetLogin()
        {
            userModel.UserLogin = _userName;
        }
    }
}
