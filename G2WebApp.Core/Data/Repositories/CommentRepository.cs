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
    public class CommentRepository : ICommentRepository
    {
        private DataContext context;

        public CommentRepository()
        {
            context = Library.GetContext();
        }

        public Comment Find(Expression<Func<Comment, bool>> filter)
        {
            return context.GetTable<Comment>().FirstOrDefault(filter);
        }

        public IEnumerable<Comment> FindAll(Expression<Func<Comment, bool>> filter = null, int? take = null)
        {
            var list = filter != null
                ? context.GetTable<Comment>().Where(filter)
                : context.GetTable<Comment>();

            return take.HasValue ? list.Take(take.Value) : list;
        }

        public bool Add(Comment entity)
        {
            context.Insert(entity);
            return true; //Exists(x => x.Content == entity.Content);
        }

        public bool Remove(Expression<Func<Comment, bool>> filter)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<Comment>().Where(filter).Delete();

            return Exists(filter);
        }

        public bool Update(Comment entity)
        {
            context.Update(entity);
            return true;
        }

        public bool UpdateManually(Expression<Func<Comment, bool>> filter, Expression<Func<Comment, object>> selector, object value)
        {
            if (!Exists(filter))
                return false;

            context.GetTable<Comment>()
                .Where(filter)
                .Set(selector, value)
                .Update();

            return true;
        }

        public bool Exists(Expression<Func<Comment, bool>> filter)
        {
            return context.GetTable<Comment>().Any(filter);
        }
    }
}