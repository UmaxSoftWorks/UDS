using System.Collections.Generic;
using System.Linq;

namespace TextGenerator
{
    class Container
    {
        private Dictionary<string, int> container;
        public Container()
        {
            this.container = new Dictionary<string, int>();
        }

        public void Add(string Item)
        {
            string item = Item.ToLower().Trim(',', '.', ';', '!', '?', '-');
            if (this.container.ContainsKey(item))
            {
                this.container[item]++;
            }
            else
            {
                this.container.Add(item, 1);
            }
        }

        public List<string> SortedItems
        {
            get
            {
                List<string> items = new List<string>();

                var results = from item in this.container
                              orderby item.Value descending
                              select item.Key;

                foreach (var result in results)
                {
                    items.Add(result);
                }
                return items;
            }
        }
    }
}
