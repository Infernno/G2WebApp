using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;

namespace G2WebApp.Core.Queries.Contracts
{
    public interface IStoryQuery
    {
        IStoryQuery Look(Expression<Func<Story, bool>> searchFilter);
        IStoryQuery BindVotes(string username);

        Task<StoryViewModel> FindAsync();
        Task<IList<StoryViewModel>> FindAllAsync(int? take = null);
        Task<FullStoryViewModel> FindFullStoryAsync();
    }
}
