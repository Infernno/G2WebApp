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
    public class StoryRepository : IStoryRepository
    {
        private DataContext context;

        public StoryRepository()
        {
            context = Library.GetContext();
        }

        public Story Find(Expression<Func<Story, bool>> filter)
        {
            return context.GetTable<Story>().FirstOrDefault(filter);
        }

        public IEnumerable<Story> FindAll(Expression<Func<Story, bool>> filter = null, int? take = null)
        {
            var list = filter != null
                ? context.GetTable<Story>().Where(filter)
                : context.GetTable<Story>();

            return take.HasValue ? list.Take(take.Value) : list;
        }

        public bool Add(Story entity)
        {
            if (Exists(x => x.Title == entity.Title))
                return false;

            context.Insert(entity);

            return Exists(x => x.Title == entity.Title);
        }

        public bool Remove(Expression<Func<Story, bool>> filter)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<Story>().Where(filter).Delete();

            return Exists(filter);
        }

        public bool Update(Story entity)
        {
            context.Update(entity);
            return true;
        }

        public bool UpdateManually(Expression<Func<Story, bool>> filter, Expression<Func<Story, object>> selector, object value)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<Story>()
                .Where(filter)
                .Set(selector, value)
                .Update();

            return true;
        }

        public bool Exists(Expression<Func<Story, bool>> filter)
        {
            return context.GetTable<Story>().Any(filter);
        }
    }
}