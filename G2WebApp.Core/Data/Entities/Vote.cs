using System;
using LinqToDB.Mapping;

namespace G2WebApp.Core.Data.Entities
{
    [Table("Votes")]
    public class Vote : Entity
    {
        [Column(Name = ("VoteType"))]
        public VoteType VoteType { get; set; }

        [Column(Name = ("EntityID"))]
        public int EntityId { get; set; }

        [Column(Name = ("Username"))]
        public string Username { get; set; }

        [Column(Name = ("VoteValue"))]
        public int VoteValue { get; set; }

        [Column(Name = ("DateAdded"))]
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
