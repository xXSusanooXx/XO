using System.ComponentModel.DataAnnotations.Schema;

namespace XO.Model.DbModels
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        [NotMapped]
        public string ConnectionId { get; set; }
    }

    public enum Role
    {
        Default = 0,
        Admin = 1
    }
}
