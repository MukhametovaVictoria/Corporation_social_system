namespace FrontEnd.Models
{
    public class CurrentUserObject
    {
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CurrentUserObject(string userName, string userID, string firstName, string lastName)
        {
            UserName = userName;
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
