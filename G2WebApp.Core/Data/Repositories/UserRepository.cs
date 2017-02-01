using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Util;
using LinqToDB;

namespace G2WebApp.Core.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext context;

        public UserRepository()
        {
            context = Library.GetContext();
        }

        public User Find(Expression<Func<User, bool>> filter)
        {
            return context.GetTable<User>().FirstOrDefault(filter);
        }

        public IEnumerable<User> FindAll(Expression<Func<User, bool>> filter = null, int? take = null)
        {
            var list = filter != null
                ? context.GetTable<User>().Where(filter)
                : context.GetTable<User>();

            return take.HasValue ? list.Take(take.Value) : list;
        }

        public bool Add(User entity)
        {
            if (Exists(x => x.Username == entity.Username))
                return false;

            context.Insert(entity);

            return Exists(x => x.Username == entity.Username);
        }
    
        public bool Remove(Expression<Func<User, bool>> filter)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<User>().Where(filter).Delete();

            return Exists(filter);
        }

        public bool Update(User entity)
        {
            context.Update(entity);
            return true;
        }

        public bool UpdateManually(Expression<Func<User, bool>> filter, Expression<Func<User, object>> selector, object value)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<User>()
                .Where(filter)
                .Set(selector, value)
                .Update();

            return true;
        }

        public bool Exists(Expression<Func<User, bool>> filter)
        {
            return context.GetTable<User>().Any(filter);
        }
    }
}