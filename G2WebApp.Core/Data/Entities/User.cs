using LinqToDB.Mapping;

namespace G2WebApp.Core.Data.Entities
{
    [Table(Name = "Users")]
    public class User : Entity
    {
        [Column(Name = ("Username"))]
        public string Username { get; set; }

        [Column(Name = ("Password"))]
        public string Password { get; set; }

        [Column(Name = "FirstName")]
        public string FirstName { get; set; }

        [Column(Name = "LastName")]
        public string LastName { get; set; }

        [Column(Name = "Class")]
        public string Class { get; set; }

        [Column(Name = ("IP"))]
        public string Ip { get; set; }

        [Column(Name = ("Role"))]
        public UserGroup Role { get; set; } = UserGroup.User;
    }
}
