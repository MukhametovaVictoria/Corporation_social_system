using FrontEnd.Services;

namespace FrontEnd.Models
{
    public class UserDataBuilderFromPA : UserDataBuilder
    {
        private readonly PersonalAccountService _personalAccountService;
        private readonly string _userId;
        private readonly string _userName;
        UserDataModel userModel = new UserDataModel();
        public UserDataBuilderFromPA(PersonalAccountService personalAccountService, string userId, string userName) {
            _personalAccountService = personalAccountService;
            _userId = userId;
            _userName = userName;
        }
        
        public override async Task BuildFromUserModel()
        {
            try
            {
                var user = await _personalAccountService.GetPersonalAccountData(_userId);
                if (user != null)
                {
                    userModel.UserModel = user;
                    userModel.FullName = user.Firstname + " " + user.Surname;
                    userModel.IsAdmin = user.IsAdmin;
                    userModel.Language = user.Language;
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
            userModel.Id = _userId;
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
