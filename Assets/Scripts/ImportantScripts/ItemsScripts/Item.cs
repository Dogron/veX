
using UnityEngine;

namespace ImportantScripts.ItemsScripts
{
    [System.Serializable]
    public class Item  
    {
        public string Name
        {
            get { return ItemGameObject.name; }
            set { ItemGameObject.name = value; }
        }

        public GameObject ItemGameObject;
   
        public int Amount;
        public string InfoAbout;
        public int IdOfObject;
  
        public void OnUse()
        {
            Amount -= 1;
         
        }
    }
}


