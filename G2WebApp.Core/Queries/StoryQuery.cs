using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Queries.Contracts;

namespace G2WebApp.Core.Queries
{
    public class StoryQuery : IStoryQuery
    {
        private IStoryRepository storyRepository;
        private ICommentRepository commentRepository;
        private IVoteRepository voteRepository;

        private Expression<Func<Story, bool>> searchFilter;
        private string username;

        public StoryQuery(IStoryRepository storyRepository, ICommentRepository commentRepository, IVoteRepository voteRepository)
        {
            this.storyRepository = storyRepository;
            this.commentRepository = commentRepository;
            this.voteRepository = voteRepository;
        }

        public IStoryQuery Look(Expression<Func<Story, bool>> SearchFilter)
        {
            searchFilter = SearchFilter;
            return this;
        }

        public IStoryQuery BindVotes(string Username)
        {
            username = Username;
            return this;
        }

        private StoryViewModel FindInternal()
        {
            var entity = storyRepository.Find(searchFilter);
            var viewModel = Mapper.Map<StoryViewModel>(entity);

            if (!string.IsNullOrEmpty(username))
            {
                var vote = voteRepository.Find(v => v.VoteType == VoteType.Story && v.EntityId == entity.Id && v.Username == username);

                if (vote != null)
                    viewModel.UserVoteValue = vote.VoteValue;
            }

            return viewModel;
        }

        private IList<StoryViewModel> FindAllInternal(int? take = null)
        {
            var entities = storyRepository.FindAll(searchFilter, take);
            var viewModels = Mapper.Map<List<StoryViewModel>>(entities)
                .OrderByDescending(s => s.OverallRating)
                .ThenByDescending(s => s.DateCreated)
                .ToList();

            if (!string.IsNullOrEmpty(username))
            {
                foreach (var viewmodel in viewModels)
                {
                    var vote = voteRepository.Find(v => v.VoteType == VoteType.Story && v.EntityId == viewmodel.Id && v.Username == username);

                    if (vote == null)
                        continue;

                    viewmodel.UserVoteValue = vote.VoteValue;
                }
            }

            return viewModels;
        }

        private FullStoryViewModel FindFullStoryInternal()
        {
            var storyEntity = storyRepository.Find(searchFilter);
            var storyViewModel = Mapper.Map<StoryViewModel>(storyEntity);

            var commentEntities = commentRepository.FindAll(c => c.StoryID == storyViewModel.Id);
            var commentViewModels = Mapper.Map<List<CommentViewModel>>(commentEntities)
                .OrderByDescending(s => s.OverallRating)
                .ThenByDescending(s => s.DateCreated)
                .ToList();

            if (!string.IsNullOrEmpty(username))
            {
                var storyVote = voteRepository.Find(v => v.VoteType == VoteType.Story && v.EntityId == storyEntity.Id && v.Username == username);

                if (storyVote != null)
                    storyViewModel.UserVoteValue = storyVote.VoteValue;

                foreach (var comment in commentViewModels)
                {
                    var commentVote = voteRepository.Find(v => v.VoteType == VoteType.Comment && v.EntityId == comment.Id && v.Username == username);

                    if (commentVote == null)
                        continue;

                    comment.UserVoteValue = commentVote.VoteValue;
                }
            }

            return new FullStoryViewModel
            {
                StoryViewModel = storyViewModel,
                CommentViewModels = commentViewModels
            };
        }

        public Task<StoryViewModel> FindAsync()
        {
            return Task.Factory.StartNew(() => FindInternal());
        }

        public Task<IList<StoryViewModel>> FindAllAsync(int? take = null)
        {
            return Task.Factory.StartNew(() => FindAllInternal(take));
        }

        public Task<FullStoryViewModel> FindFullStoryAsync()
        {
            return Task.Factory.StartNew(() => FindFullStoryInternal());
        }
    }
}