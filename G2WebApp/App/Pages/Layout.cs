using System.Collections.Generic;

namespace G2WebApp.App.Pages
{
    public class Layout
    {
        public string Name { get; set; }
        public List<Section> Sections { get; set; }
    }

    public class Section
    {
        public int Index { get; set; }
        public Item Item { get; set; }
    }

    public class Item
    {
        public string PartialName { get; set; }
        public dynamic Data { get; set; }
    }
}
