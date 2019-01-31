using System.Collections.Generic;
using ImportantScripts.ItemsScripts;
using UnityEngine;

namespace ImportantScripts.CharScripts
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> ItemsInInventory;
        public int SizeOfInventory;

        public List<Item> MatchedItems;
    
    
        public void AddToInventory(Item item)
        {
            foreach (var itemInInventory in ItemsInInventory)
            {
                if (itemInInventory.IdOfObject != item.IdOfObject) continue;
                itemInInventory.Amount += 1;
                return;
            }
            ItemsInInventory.Add(item);
        }

        public void RemoveFromInventory(Item item)
        {
            ItemsInInventory.Remove(item);
        }
    
        private void FixedUpdate()
        {
            MatchedItems.Clear();
       
            foreach (var item in ItemsInInventory)
            {
                if (item.Amount == 0)
                {
                    MatchedItems.Add(item); 
                }
            }

            if (MatchedItems != null)
            {
                foreach (var matchedItem in MatchedItems)
                {
                    RemoveFromInventory(matchedItem);
                } 
            } 
        }
    }
}