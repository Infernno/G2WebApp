using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using G2WebApp.Core.Data.Entities;

namespace G2WebApp.Core.Data.Contracts
{
    public interface IProfileRepository
    {
        Profile Find(Expression<Func<Profile, bool>> filter);
        IEnumerable<Profile> FindAll(Expression<Func<Profile, bool>> filter = null, int? take = null);
        bool Add(Profile entity);
        bool Remove(Expression<Func<Profile, bool>> filter);
        bool Update(Profile entity);
        bool UpdateManually(Expression<Func<Profile, bool>> filter, Expression<Func<Profile, object>> selector, object value);
        bool Exists(Expression<Func<Profile, bool>> filter);
    }
}
