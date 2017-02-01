using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using G2WebApp.Core.Data.Entities;

namespace G2WebApp.Core.Data.ViewModels
{
    public class StoryViewModel
    {
        public int Id { get; set; }

        public ContentType StoryType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CommentsCount { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string Tags { get; set; }

        public int OverallRating { get; set; }

        public int? UserVoteValue { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }
    }

    public class FullStoryViewModel
    {
        public StoryViewModel StoryViewModel { get; set; }
        public List<CommentViewModel> CommentViewModels { get; set; }
    }

    public class NewTextStoryViewModel
    {
        public string Author { get; set; }

        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Введите теги")]
        public string Tags { get; set; }

        [Required(ErrorMessage = "Введите содержимое")]
        public string Text { get; set; }
    }

    public class NewImageStoryViewModel
    {
        public string Author { get; set; }

        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Введите теги")]
        public string Tags { get; set; }

        public string ImageName { get; set; }

        public Stream ImageStream { get; set; }
    }
}
