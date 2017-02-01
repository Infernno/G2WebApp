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
    public class VoteRepository : IVoteRepository
    {
        private DataContext context;

        public VoteRepository()
        {
            context = Library.GetContext();
        }

        public Vote Find(Expression<Func<Vote, bool>> filter)
        {
            return context.GetTable<Vote>().FirstOrDefault(filter);
        }

        public IEnumerable<Vote> FindAll(Expression<Func<Vote, bool>> filter = null, int? take = null)
        {
            var list = filter != null
                ? context.GetTable<Vote>().Where(filter)
                : context.GetTable<Vote>();

            return take.HasValue ? list.Take(take.Value) : list;
        }


        public bool Add(Vote entity)
        {
            if (Exists(x => x.EntityId == entity.EntityId && x.Username == entity.Username))
                return false;

            context.Insert(entity);

            return Exists(x => x.EntityId == entity.EntityId && x.Username == entity.Username);
        }


        public bool Remove(Expression<Func<Vote, bool>> filter)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<Vote>().Where(filter).Delete();

            return Exists(filter);
        }

        public bool Update(Vote entity)
        {
            context.Update(entity);
            return true;
        }

        public bool UpdateManually(Expression<Func<Vote, bool>> filter, Expression<Func<Vote, object>> selector, object value)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<Vote>()
                .Where(filter)
                .Set(selector, value)
                .Update();

            return true;
        }

        public bool Exists(Expression<Func<Vote, bool>> filter)
        {
            return context.GetTable<Vote>().Any(filter);
        }
    }
}
