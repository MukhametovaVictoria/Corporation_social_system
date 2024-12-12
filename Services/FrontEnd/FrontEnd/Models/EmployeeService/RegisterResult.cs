namespace FrontEnd.Models.EmployeeService
{
    public class RegisterResult
    {
        public bool IsSuccess { get; set; }
        public bool LoginExist { get; set; }
        public bool EmailExist { get; set; }
        public bool SimplePassword { get; set; }
        public bool PasswordContainsUsername { get; set; }
        public bool IsNotAppropriateLogin { get; set; }
        public bool IsNotAppropriatePassword { get; set; }
    }
}
