using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using G2WebApp.Core.Data.Entities;

namespace G2WebApp.Core.Data.Contracts
{
    public interface IStoryRepository
    {
        Story Find(Expression<Func<Story, bool>> filter);
        IEnumerable<Story> FindAll(Expression<Func<Story, bool>> filter = null, int? take = null);
        bool Add(Story entity);
        bool Remove(Expression<Func<Story, bool>> filter);
        bool Update(Story entity);
        bool UpdateManually(Expression<Func<Story, bool>> filter, Expression<Func<Story, object>> selector, object value);
        bool Exists(Expression<Func<Story, bool>> filter);
    }
}
