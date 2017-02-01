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
    public class ProfileRepository : IProfileRepository
    {
        private DataContext context;

        public ProfileRepository()
        {
            context = Library.GetContext();
        }

        public Profile Find(Expression<Func<Profile, bool>> filter)
        {
            return context.GetTable<Profile>().FirstOrDefault(filter);
        }

        public IEnumerable<Profile> FindAll(Expression<Func<Profile, bool>> filter = null, int? take = null)
        {
            var list = filter != null
                ? context.GetTable<Profile>().Where(filter)
                : context.GetTable<Profile>();

            return take.HasValue ? list.Take(take.Value) : list;
        }

        public bool Add(Profile entity)
        {
            if (Exists(x => x.DisplayName == entity.DisplayName))
                return false;

            context.Insert(entity);

            return Exists(x => x.DisplayName == entity.DisplayName);
        }

        public bool Remove(Expression<Func<Profile, bool>> filter)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<Profile>().Where(filter).Delete();

            return Exists(filter);
        }

        public bool Update(Profile entity)
        {
            context.Update(entity);
            return true;
        }

        public bool UpdateManually(Expression<Func<Profile, bool>> filter, Expression<Func<Profile, object>> selector, object value)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<Profile>()
                .Where(filter)
                .Set(selector, value)
                .Update();

            return true;
        }

        public bool Exists(Expression<Func<Profile, bool>> filter)
        {
            return context.GetTable<Profile>().Any(filter);
        }
    }
}