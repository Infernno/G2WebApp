using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;

namespace G2WebApp.Core.Queries.Contracts
{
    public interface ICommentQuery
    {
        ICommentQuery Look(Expression<Func<Comment, bool>> searchFilter);
        ICommentQuery BindVotes(string Username);

        Task<CommentViewModel> FindAsync();
        Task<IList<CommentViewModel>> FindAllAsync();
    }
}
