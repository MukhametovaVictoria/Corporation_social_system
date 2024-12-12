namespace TestIdentity.Models
{
    public class ResetPasswordResponseModel
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
