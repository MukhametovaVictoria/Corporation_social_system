namespace FrontEnd.Models.Auth
{
    public class ResetPasswordResponseModel
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
