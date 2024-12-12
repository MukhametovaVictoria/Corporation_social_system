namespace FrontEnd.Models
{
    public class BuilderOwner
    {
        UserDataBuilder builder;
        public BuilderOwner(UserDataBuilder builder)
        {
            this.builder = builder;
        }
        public async Task Construct()
        {
            builder.SetLogin();
            await builder.BuildFromUserModel();
            builder.SetLanguage();
            builder.SetAdditional();
        }
    }
}
