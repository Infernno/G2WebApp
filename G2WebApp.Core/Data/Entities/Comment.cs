using System;
using LinqToDB.Mapping;

namespace G2WebApp.Core.Data.Entities
{
    [Table("Comments")]
    public class Comment : Entity
    {
        [Column(Name = "StoryID")]
        public int StoryID { get; set; }

        [Column(Name = "Author")]
        public string Author { get; set; }

        [Column(Name = "OverallRating")]
        public int OverallRating { get; set; }

        [Column(Name = "Flag")]
        public FlagType Flag { get; set; } = FlagType.IsWaiting;

        [Column(Name = "CommentType")]
        public ContentType CommentType { get; set; }

        [Column(Name = "Content")]
        public string Content { get; set; }

        [Column(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}