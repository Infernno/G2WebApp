using System.Collections.Generic;

namespace G2WebApp.Core.Data.ViewModels
{
    public class SearchResultViewModel
    {
        public string Query { get; set; }
        public List<StoryViewModel> PostViewModels { get; set; }
    }
}
