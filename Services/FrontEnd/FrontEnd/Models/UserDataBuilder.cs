namespace FrontEnd.Models
{
    public abstract class UserDataBuilder
    {
        public abstract Task BuildFromUserModel();
        public abstract void SetLogin();
        public abstract void SetLanguage();
        public abstract void SetAdditional();
        public abstract UserDataModel GetResult();
    }
}
