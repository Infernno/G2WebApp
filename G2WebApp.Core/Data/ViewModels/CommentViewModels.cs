using System;
using System.ComponentModel.DataAnnotations;

namespace G2WebApp.Core.Data.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public int OverallRating { get; set; }

        public int? UserVoteValue { get; set; }

        /* public ContentType CommentType { get; set; } */

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class CommentOfDayViewModel
    {
        public string StoryTitle { get; set; }

        public int StoryID { get; set; }

        public string Author { get; set; }

        public int OverallRating { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class AddCommentViewModel
    {
        public int StoryID { get; set; }

        public string Author { get; set; }

        [Required(ErrorMessage = "Комментарий пустой")]
        public string Content { get; set; }
    }
}
