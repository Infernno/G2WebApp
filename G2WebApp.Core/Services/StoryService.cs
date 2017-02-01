using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.FileSystem;
using G2WebApp.Core.Services.Contracts;
using G2WebApp.Core.Util;

namespace G2WebApp.Core.Services
{
    public class StoryService : IStoryService
    {
        private IStoryRepository storyRepository;
        private IVoteRepository voteRepository;

        private static readonly object syncLock = new object();

        public StoryService(IStoryRepository storyRepository, IVoteRepository voteRepository)
        {
            this.storyRepository = storyRepository;
            this.voteRepository = voteRepository;
        }

        private OperationResult AddStoryInternal(object viewModel, ContentType StoryType)
        {
            lock (syncLock)
            {
                switch (StoryType)
                {
                    case ContentType.Text:
                        {
                            var storyViewModel = (NewTextStoryViewModel)viewModel;
                            var storyEntity = Mapper.Map<Story>(storyViewModel);

                            storyEntity.StoryType = ContentType.Text;

                            var isCreated = storyRepository.Add(storyEntity);

                            if (isCreated)
                            {
                                var storyId = storyRepository.Find(s => s.Title == storyEntity.Title).Id;
                                return OperationResult.Successful(storyId);
                            }

                            return OperationResult.Failed("Ошибка создания поста, возможно такой пост уже существует", null);
                        }

                    case ContentType.Image:
                        {
                            var storyViewModel = (NewImageStoryViewModel)viewModel;
                            var storyEntity = Mapper.Map<Story>(storyViewModel);

                            storyEntity.StoryType = ContentType.Image;

                            try
                            {
                                var image = new Image(storyViewModel.ImageName, storyViewModel.ImageStream);
                                image.Upload();

                                storyEntity.ImageUrl = image.UrlPath;
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError("Failed to upload image: " + ex);
                                return OperationResult.Failed("Произошла внутреняя ошибка сервера при загрузке изображения...");
                            }

                            var isStoryCreated = storyRepository.Add(storyEntity);

                            if (isStoryCreated)
                            {
                                var storyId = storyRepository.Find(s => s.Title == storyEntity.Title).Id;
                                return OperationResult.Successful(storyId);
                            }

                            return OperationResult.Failed("Ошибка создания поста, возможно такой пост уже существует");
                        }

                    default:
                        return null;
                }
            }
        }

        private OperationResult VoteInternal(VoteParams vote)
        {
            lock (syncLock)
            {
                var currentVote = voteRepository.Find(v => v.VoteType == VoteType.Story && v.EntityId == vote.EntityID && v.Username == vote.Username);

                if (currentVote == null)
                {
                    var newVote = new Vote
                    {
                        VoteType = VoteType.Story,
                        EntityId = vote.EntityID,
                        VoteValue = vote.VoteValue,
                        Username = vote.Username
                    };

                    voteRepository.Add(newVote);
                }
                else
                {
                    if (vote.VoteValue == 0)
                        voteRepository.Remove(v => v.EntityId == vote.EntityID && v.VoteType == VoteType.Story && v.Username == vote.Username);
                    else
                        voteRepository.UpdateManually(v => v.EntityId == vote.EntityID && v.VoteType == VoteType.Story && v.Username == vote.Username, v => v.VoteValue, vote.VoteValue);
                }

                var rating = voteRepository.FindAll(v => v.EntityId == vote.EntityID && v.VoteType == VoteType.Story).Sum(p => p.VoteValue);
                var result = storyRepository.UpdateManually(p => p.Id == vote.EntityID, p => p.OverallRating, rating);

                if (!result)
                    return OperationResult.Failed("Упс, что-то пошло не так...");

                return OperationResult.Successful(rating);
            }
        }

        public Task<OperationResult> AddStory(object viewModel, ContentType StoryType)
        {
            return Task.Factory.StartNew(() => AddStoryInternal(viewModel, StoryType));
        }

        public Task<OperationResult> Vote(VoteParams vote)
        {
            return Task.Factory.StartNew(() => VoteInternal(vote));
        }
    }
}
