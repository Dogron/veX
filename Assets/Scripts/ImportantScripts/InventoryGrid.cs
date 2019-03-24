using ImportantScripts.ItemsScripts;
using ImportantScripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace ImportantScripts
{
    public class InventoryGrid : MonoBehaviour
    {
        public Item itemInThisGrid;
     
        public void OnButtonPressed()
        {
            InventoryManager.InventoryManagerIn.theGridWhatHaveBeenChosed = gameObject;
        }

        public void ChooseThisGrid()
        {
            InventoryManager.InventoryManagerIn.theGridWhatHaveBeenChosed = gameObject;
        }
        
        private void Update()
        {
            if (itemInThisGrid != null)
            {
                if (itemInThisGrid.amountOfItem <= 0)
                {
                    itemInThisGrid = null;
                    UpdateInfoOfPartOfGrid();
                }
                
                UpdateInfoOfPartOfGrid();
            }      
        }

        public void UpdateInfoOfPartOfGrid()
        {
            if (itemInThisGrid != null)
            {
                GetComponentInChildren<Text>().text = itemInThisGrid.amountOfItem + "   " + itemInThisGrid.Name;
            }

            else
            {
                GetComponentInChildren<Text>().text = "";
            }
        }
    }
}