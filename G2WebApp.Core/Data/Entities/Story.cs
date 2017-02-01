using System;
using LinqToDB.Mapping;

namespace G2WebApp.Core.Data.Entities
{
    [Table("Stories")]
    public class Story : Entity
    {
        [Column(Name = "StoryType")]
        public ContentType StoryType { get; set; }

        [Column(Name = "Flag")]
        public FlagType Flag { get; set; } = FlagType.IsWaiting;

        [Column(Name = "Title")]
        public string Title { get; set; }

        [Column(Name = "Description")]
        public string Description { get; set; }

        [Column(Name = "Tags")]
        public string Tags { get; set; }

        [Column(Name = "Author")]
        public string Author { get; set; }

        [Column(Name = "OverallRating")]
        public int OverallRating { get; set; }

        [Column(Name = "Text")]
        public string Text { get; set; }

        [Column(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [Column(Name = "VideoUrl")]
        public string VideoUrl { get; set; }

        [Column(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
