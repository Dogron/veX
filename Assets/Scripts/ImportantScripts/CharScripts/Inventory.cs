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
            if (item.ItemType == ItemsTypes.Xp)
            {
              gameObject.GetComponent<Char>().GetXp(item.amountOfResource);
              return;
            }

            if (item.ItemType == ItemsTypes.PocketWithMoney)
            {
                gameObject.GetComponent<Char>().Money += item.amountOfResource * item.amountOfItem;
                return;
            }
            
            if (item.IsItNoStaking == false)
            {
                foreach (var itemInInventory in ItemsInInventory)
                {
                    if (itemInInventory.IsItNoStaking == false)
                    {
                        if (itemInInventory.Name != item.Name) continue;
                
                        itemInInventory.amountOfItem += item.amountOfItem;
                        return; 
                    }
                }
            }
            
            ItemsInInventory.Add(new Item(item.itemGameObject,item.amountOfItem,item.infoAbout,item.amountOfResource,item.isUseble,item.moneyCost,item.ItemType,item.IsItNoStaking,item.Name));
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
                if (item.amountOfItem == 0)
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