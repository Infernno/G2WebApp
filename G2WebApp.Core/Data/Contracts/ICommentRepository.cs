using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using G2WebApp.Core.Data.Entities;

namespace G2WebApp.Core.Data.Contracts
{
    public interface ICommentRepository
    {
        Comment Find(Expression<Func<Comment, bool>> filter);
        IEnumerable<Comment> FindAll(Expression<Func<Comment, bool>> filter = null, int? take = null);
        bool Add(Comment entity);
        bool Remove(Expression<Func<Comment, bool>> filter);
        bool Update(Comment entity);
        bool UpdateManually(Expression<Func<Comment, bool>> filter, Expression<Func<Comment, object>> selector, object value);
        bool Exists(Expression<Func<Comment, bool>> filter);
    }
}
