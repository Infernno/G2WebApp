using System;
using System.Collections.Generic;

namespace G2WebApp.Core.Data.ViewModels
{
    public class ProfileViewModel
    {
        public string DisplayName { get; set; }

        public string PhotoFile { get; set; }

        public int Rating { get; set; }

        public DateTime RegistrationDate { get; set; }
    }

    public class PublicProfileViewModel
    {
        public ProfileViewModel Profile { get; set; }
        public List<StoryViewModel> Posts { get; set; } 
    }

    public class ProfileTabViewModel
    {
        public string DisplayName { get; set; }

        public string PhotoFile { get; set; }

        public int Rating { get; set; }
    }

    public class EditProfileViewModel
    {

    }
}
