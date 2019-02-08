
using UnityEngine;

namespace ImportantScripts.ItemsScripts
{
    [System.Serializable]
    public class Item 
    {
        public string Name
        {
            get { return ItemGameObject.name; }
        }

        public GameObject ItemGameObject;
   
        public int AmountOfItem;
        public string InfoAbout;

        public int AmountOfResource;

        public bool IsUseble;

        public int MoneyCost;
        
        public ItemsTypes ItemType;

        public bool IsItNoStaking;

        public int IDofObject;
        
        public int OnUse()
        {
            if (!IsUseble) return 0;
            Remove();

            return AmountOfResource;
        }

        private void Remove()
        {
            AmountOfItem -= 1;
        }

        public Item(GameObject itemGameObject, int amountOfItem, string infoAbout, int amountOfResource, 
            bool isUseble, int moneyCost, ItemsTypes itemType, int idDofObject)
        {
            ItemGameObject = itemGameObject;
            AmountOfItem = amountOfItem;
            InfoAbout = infoAbout;
            AmountOfResource = amountOfItem;
            IsUseble = isUseble;
            MoneyCost = moneyCost;
            ItemType = itemType;
            IDofObject = idDofObject;
        }
    }
}


