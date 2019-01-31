using ImportantScripts.ItemsScripts;
using ImportantScripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace ImportantScripts
{
    public class InventoryGrid : MonoBehaviour
    {
        public Item ItemInThisGrid;
     
        public void OnButtonPressed()
        {
            InventoryManager.InventoryManagerIn.TheGridWhatHaveBeenChosed = gameObject;
        }

        public void ChooseThisGrid()
        {
            CanvasManager.CanvasManagerIn.TheGridWhatHaveBeenChosed = gameObject;
        }
        
        private void Update()
        {
            if (ItemInThisGrid != null)
            {
                if (ItemInThisGrid.Amount <= 0)
                {
                    ItemInThisGrid = null;
                }
            }      
        
            UpdateInfoOfPartOfGrid();
        }

        public void UpdateInfoOfPartOfGrid()
        {
            if (ItemInThisGrid != null)
            {
                GetComponentInChildren<Text>().text = ItemInThisGrid.Amount + "   " + ItemInThisGrid.Name;
            }

            else
            {
                GetComponentInChildren<Text>().text = "";
            }
        }
    }
}