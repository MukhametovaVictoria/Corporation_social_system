using FrontEnd.Models.PersonalAccountModels;

namespace FrontEnd.Models
{
    public class UserDataModel
    {
        public string Id { get; set; }
        public Dictionary<string, string> Captions { get; set; }
        public string Language { get; set; }
        public string FullName { get; set; }
        public EmployeeModelFromPA UserModel { get; set; }
        public bool IsAdmin { get; set; }
        public string UserLogin { get; set; }
    }
}
