using System;
using LinqToDB.Mapping;

namespace G2WebApp.Core.Data.Entities
{
    [Table(Name = "Profiles")]
    public class Profile : Entity
    {
        [Column(Name = "DisplayName")]
        public string DisplayName { get; set; }

        [Column(Name = "PhotoFile")]
        public string PhotoFile { get; set; } = "/Images/Avatars/noAvatar.png";

        [Column(Name = "Rating")]
        public int Rating { get; set; } = 0;

        [Column(Name = "RegistrationDate")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}