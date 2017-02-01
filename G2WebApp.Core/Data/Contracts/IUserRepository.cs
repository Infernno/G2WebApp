using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using G2WebApp.Core.Data.Entities;

namespace G2WebApp.Core.Data.Contracts
{
    public interface IUserRepository
    {
        User Find(Expression<Func<User, bool>> filter);
        IEnumerable<User> FindAll(Expression<Func<User, bool>> filter = null, int? take = null);
        bool Add(User entity);
        bool Remove(Expression<Func<User, bool>> filter);
        bool Update(User entity);
        bool UpdateManually(Expression<Func<User, bool>> filter, Expression<Func<User, object>> selector, object value);
        bool Exists(Expression<Func<User, bool>> filter);
    }
}
