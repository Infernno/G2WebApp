using System;
using System.Linq;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.Repositories;
using G2WebApp.Core.Util;

namespace G2WebApp.Core.BackgroundTasks.Tasks
{
    public class CleanupTask : Job
    {
        public override bool IsRepeatable => true;
        public override TimeSpan RepetitionIntervalTime => TimeSpan.FromMinutes(5);

        private IStoryRepository storyRepository = new StoryRepository();
        private ICommentRepository commentRepository = new CommentRepository();

        public override void Action()
        {
            var trashPosts = storyRepository.FindAll(p => p.Flag == FlagType.Delete).ToList();
            commentRepository.Remove(c => c.Flag == FlagType.Delete);

            for (int i = 0; i < trashPosts.Count; i++)
            {
                var id = trashPosts[i].Id;

                storyRepository.Remove(p => p.Id == id);
                commentRepository.Remove(c => c.StoryID == id);
            }

            Debug.Log("There are total {0} posts that were removed.", trashPosts.Count);
        }
    }
}
