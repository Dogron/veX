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
              gameObject.GetComponent<Char>().GetXp(item.AmountOfResource);
              return;
            }
            
            if (item.IsItNoStaking == false)
            {
                foreach (var itemInInventory in ItemsInInventory)
                {
                    if (itemInInventory.IsItNoStaking == false)
                    {
                        if (itemInInventory.IDofObject != item.IDofObject) continue;
                        if (item.ItemType == ItemsTypes.PocketWithMoney)
                        {
                            itemInInventory.AmountOfResource += item.AmountOfResource * item.AmountOfItem;
                            return;
                        }
                
                        itemInInventory.AmountOfItem += item.AmountOfItem;
                        return; 
                    }
                }
            }

            if (item.ItemType == ItemsTypes.PocketWithMoney)
            {
                ItemsInInventory.Add(new Item(item.ItemGameObject,1,item.InfoAbout,item.AmountOfItem * item.AmountOfResource,item.IsUseble,item.MoneyCost,item.ItemType,item.IDofObject,item.IsItNoStaking)); 
            }
            
            ItemsInInventory.Add(new Item(item.ItemGameObject,item.AmountOfItem,item.InfoAbout,item.AmountOfResource,item.IsUseble,item.MoneyCost,item.ItemType,item.IDofObject,item.IsItNoStaking));
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
                if (item.AmountOfItem == 0)
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