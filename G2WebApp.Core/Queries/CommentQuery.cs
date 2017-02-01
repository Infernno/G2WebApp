using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Queries.Contracts;

namespace G2WebApp.Core.Queries
{
    public class CommentQuery : ICommentQuery
    {
        private ICommentRepository commentRepository;
        private IVoteRepository voteRepository;

        private Expression<Func<Comment, bool>> searchFilter;
        private string Username;

        public CommentQuery(ICommentRepository commentRepository, IVoteRepository voteRepository)
        {
            this.commentRepository = commentRepository;
            this.voteRepository = voteRepository;
        }

        public ICommentQuery Look(Expression<Func<Comment, bool>> searchFilter)
        {
            this.searchFilter = searchFilter;
            return this;
        }

        public ICommentQuery BindVotes(string Username)
        {
            this.Username = Username;
            return this;
        }

        private CommentViewModel FindInternal()
        {
            var commentEntity = commentRepository.Find(searchFilter);
            var commentViewModel = Mapper.Map<CommentViewModel>(commentEntity);

            if (!string.IsNullOrEmpty(Username))
            {
                var vote =
                    voteRepository.Find(
                        v =>
                            v.EntityId == commentViewModel.Id && v.VoteType == VoteType.Comment &&
                            v.Username == Username);

                if (vote != null)
                {
                    commentViewModel.UserVoteValue = vote.VoteValue;
                }
            }

            return commentViewModel;
        }

        private IList<CommentViewModel> FindAllInternal()
        {
            var commentEntities = commentRepository.FindAll(searchFilter);
            var commentViewModels = Mapper.Map<List<CommentViewModel>>(commentEntities);

            if (!string.IsNullOrEmpty(Username))
            {
                foreach (var commentViewModel in commentViewModels)
                {
                    var vote =
                        voteRepository.Find(
                            v =>
                                v.EntityId == commentViewModel.Id && v.VoteType == VoteType.Comment &&
                                v.Username == Username);

                    if(vote == null)
                        continue;

                    commentViewModel.UserVoteValue = vote.VoteValue;
                }
            }

            return commentViewModels;
        }

        public Task<CommentViewModel> FindAsync()
        {
            return Task.Factory.StartNew(() => FindInternal());
        }

        public Task<IList<CommentViewModel>> FindAllAsync()
        {
            return Task.Factory.StartNew(() => FindAllInternal());
        }
    }
}
