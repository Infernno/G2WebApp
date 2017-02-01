using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Services.Contracts;
using G2WebApp.Core.Util;

namespace G2WebApp.Core.Services
{
    public class CommentService : ICommentService
    {
        private ICommentRepository commentRepository;
        private IVoteRepository voteRepository;

        private static object syncLock = new object();

        public CommentService(ICommentRepository commentRepository, IVoteRepository voteRepository)
        {
            this.commentRepository = commentRepository;
            this.voteRepository = voteRepository;
        }

        private OperationResult AddCommentInternal(AddCommentViewModel model)
        {
            lock (syncLock)
            {
                var commentEntity = Mapper.Map<Comment>(model);
                var isCreated = commentRepository.Add(commentEntity);

                if (isCreated)
                    return OperationResult.Successful("Ваш комментарий успешно добавлен и ожидает проверки.");

                return OperationResult.Failed("Произошла ошибка...");
            }
        }

        private OperationResult VoteInternal(VoteParams vote)
        {
            lock (syncLock)
            {
                var currentVote =
                    voteRepository.Find(
                        v => v.VoteType == VoteType.Comment && v.EntityId == vote.EntityID && v.Username == vote.Username);

                if (currentVote == null)
                {
                    var newVote = new Vote
                    {
                        VoteType = VoteType.Comment,
                        EntityId = vote.EntityID,
                        VoteValue = vote.VoteValue,
                        Username = vote.Username
                    };

                    voteRepository.Add(newVote);
                }
                else
                {
                    if (vote.VoteValue == 0)
                        voteRepository.Remove(
                            v =>
                                v.EntityId == vote.EntityID && v.VoteType == VoteType.Comment &&
                                v.Username == vote.Username);
                    else
                        voteRepository.UpdateManually(
                            v =>
                                v.EntityId == vote.EntityID && v.VoteType == VoteType.Comment &&
                                v.Username == vote.Username, v => v.VoteValue, vote.VoteValue);
                }

                var rating =
                    voteRepository.FindAll(v => v.EntityId == vote.EntityID && v.VoteType == VoteType.Comment)
                        .Sum(p => p.VoteValue);
                var result = commentRepository.UpdateManually(p => p.Id == vote.EntityID, p => p.OverallRating, rating);

                if (!result)
                    return OperationResult.Failed("Упс, что-то пошло не так...");

                return OperationResult.Successful(rating);
            }
        }

        public Task<OperationResult> AddComment(AddCommentViewModel model)
        {
            return Task.Factory.StartNew(() => AddCommentInternal(model));
        }

        public Task<OperationResult> Vote(VoteParams vote)
        {
            return Task.Factory.StartNew(() => VoteInternal(vote));
        }
    }
}
