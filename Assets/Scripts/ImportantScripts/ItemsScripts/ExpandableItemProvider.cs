using System.Collections.Generic;
using ImportantScripts.ItemsScripts;

namespace ResourcesAndItems
{
    public class ExpandableItemProvider : ItemProvider
    {
        public override List<Item> Consume()
        {
            return ItemsInProvider;
        }
    }
}