using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using G2WebApp.Core.Data.Entities;

namespace G2WebApp.Core.Data.Contracts
{
    public interface IVoteRepository
    {
        Vote Find(Expression<Func<Vote, bool>> filter);
        IEnumerable<Vote> FindAll(Expression<Func<Vote, bool>> filter = null, int? take = null);
        bool Add(Vote entity);
        bool Remove(Expression<Func<Vote, bool>> filter);
        bool Update(Vote entity);
        bool UpdateManually(Expression<Func<Vote, bool>> filter, Expression<Func<Vote, object>> selector, object value);
        bool Exists(Expression<Func<Vote, bool>> filter);
    }
}
