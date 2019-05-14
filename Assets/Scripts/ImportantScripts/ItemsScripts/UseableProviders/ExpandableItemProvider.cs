using System.Collections.Generic;
using System.Linq;
using ImportantScripts.ItemsScripts;

namespace ResourcesAndItems
{
    public class ExpandableItemProvider : ItemProvider
    {
        public string Name;
        
        public override List<Item> Consume()
        {
            return ItemsInProvider;
        }

        private void Update()
        {
            if (ItemsInProvider.Count== 0)
            {
              gameObject.SetActive(false);
            }
        }
    }
}