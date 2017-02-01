using System;
using System.Linq;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.DependencyManagement;
using G2WebApp.Core.Util;

namespace G2WebApp.Core.BackgroundTasks.Tasks
{
    public class RecalcuteRatingTask : Job
    {
        public override bool IsRepeatable => true;
        public override TimeSpan RepetitionIntervalTime => TimeSpan.FromMinutes(4);

        private IProfileRepository profileRepository = DependencyResolver.Current.Resolve<IProfileRepository>();
        private IStoryRepository storyRepository = DependencyResolver.Current.Resolve<IStoryRepository>();
        private ICommentRepository commentRepository = DependencyResolver.Current.Resolve<ICommentRepository>();

        public override void Action()
        {
            var users = profileRepository.FindAll().ToList();

            for (int i = 0; i < users.Count; i++)
            {
                var currentUser = users[i];

                var storiesRating = storyRepository.FindAll(s => s.Author == currentUser.DisplayName).Sum(s => s.OverallRating);
                var commentRating = commentRepository.FindAll(c => c.Author == currentUser.DisplayName).Sum(c => c.OverallRating);

                profileRepository.UpdateManually(p => p.DisplayName == currentUser.DisplayName, p => p.Rating,
                    storiesRating + commentRating);      
            }

            Debug.Log("Successfully updated rating for {0} users", users.Count);
        }
    }
}
