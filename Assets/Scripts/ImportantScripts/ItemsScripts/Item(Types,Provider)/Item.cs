
using UnityEngine;

namespace ImportantScripts.ItemsScripts
{
    [System.Serializable]

public class Item
    {
        public string Name;
       
        public GameObject itemGameObject;
   
        public int amountOfItem;
        
        public string infoAbout;

        public int amountOfResource;

        public bool isUseble;

        public int moneyCost;
        
        public ItemsTypes ItemType;

        public bool IsItNoStaking;

        

        public int OnUse()
        {
            if (!isUseble) return 0;
            Remove();

            return amountOfResource;
        }

        private void Remove()
        {
            amountOfItem -= 1;
        }

        public Item(GameObject itemGameObject, int amountOfItem, string infoAbout, int amountOfResource, 
            bool isUseble, int moneyCost, ItemsTypes itemType, bool isItNoStaking,string name)
        {
            this.itemGameObject = itemGameObject;
            this.amountOfItem = amountOfItem;
            this.infoAbout = infoAbout;
            this.amountOfResource = amountOfResource;
            this.isUseble = isUseble;
            this.moneyCost = moneyCost;
            ItemType = itemType;
            IsItNoStaking = isItNoStaking;
            Name = name;
        }

        public Item(Item item)
        {
            Name = item.Name;
            itemGameObject = item.itemGameObject;
            amountOfItem = item.amountOfItem;
            infoAbout = item.infoAbout;
            amountOfResource = item.amountOfResource;
            isUseble = item.isUseble;
            moneyCost = item.moneyCost;
            ItemType = item.ItemType;
            IsItNoStaking = item.IsItNoStaking;
            Name = item.Name;
        }
    }
}


