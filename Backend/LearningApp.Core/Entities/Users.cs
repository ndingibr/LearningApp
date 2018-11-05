namespace LearningApp.Core.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Police_Station { get; set; }
        public bool IsActive{ get; set; }
    }
}
