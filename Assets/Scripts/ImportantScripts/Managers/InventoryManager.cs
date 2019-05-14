using System.Collections.Generic;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ImportantScripts.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager InventoryManagerIn;

        public GameObject inventorySpace;
        public List<InventoryGrid> inventoryGrid;

        public Selectable firstElementToSelect;
        
        public GameObject infoText;
        public InventoryGrid theGridWhatHaveBeenChoosed;
        
        
        public GameObject helpPanelInventory;
        
        private void Awake()
        {
            InventoryManagerIn = this;
        }
       
        public void InventoryOnOff(bool onOff)
        {
            theGridWhatHaveBeenChoosed = null;
            CanvasManager.CanvasManagerIn.CloseEverything();
            inventorySpace.SetActive(onOff);
            infoText.GetComponent<Text>().text = "";

            if (inventorySpace.activeInHierarchy)
            {
                firstElementToSelect.Select();
                UpdateInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>());
            }
        }

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                var isActive = inventorySpace.activeInHierarchy;
                InventoryOnOff(!isActive);
            }

            if (CanvasManager.CanvasManagerIn.inventorySpace.activeInHierarchy)
            {
                UpdateInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>());
                
                if (Input.GetKeyDown(KeyCode.C))
                {
	                print("Enter has pressed");
                					
	                if (EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>() != null)
	                {
		                print("The Grid has been Chosed");
		                theGridWhatHaveBeenChoosed = EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>();
		                HelpPanelOnOff(true,theGridWhatHaveBeenChoosed.transform);
	                }
                }

                if (Input.GetKeyDown(KeyCode.K))
                {
                    foreach (var craftGrid in  CraftManager.CraftManagerIn.craftGrid)
                    {
                        if (craftGrid.itemInThisGrid == null)
                        {
                            craftGrid.itemInThisGrid = new Item(theGridWhatHaveBeenChoosed.itemInThisGrid);
                            theGridWhatHaveBeenChoosed.itemInThisGrid.amountOfItem = 0;
                            break;
                        }
                    }
                }
                 
                if (Input.GetKeyDown(KeyCode.J))
                {
	                print("K has pressed");
	                infoText.GetComponent<Text>().text = theGridWhatHaveBeenChoosed.itemInThisGrid != null ? theGridWhatHaveBeenChoosed.itemInThisGrid.infoAbout : "";
                }

                if (Input.GetKeyDown(KeyCode.L))
                {
	                if (theGridWhatHaveBeenChoosed.GetComponent<InventoryGrid>().itemInThisGrid != null)
	                {
		                Char.CharIn.OnUseItem(theGridWhatHaveBeenChoosed.itemInThisGrid);
	                }
	                HelpPanelOnOff(false,null);
                }
                				
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (theGridWhatHaveBeenChoosed.itemInThisGrid != null)
                        theGridWhatHaveBeenChoosed.itemInThisGrid.amountOfItem -= 1;
                    HelpPanelOnOff(false,null);
                }
            }
        }

        public void UpdateInventory(Inventory inventory)
        {
            foreach (var grid in inventoryGrid)
            {
                grid.itemInThisGrid = null;
            }
            
            for (var i = 0; i < inventoryGrid.Count; i++)
            {
                if (inventory.ItemsInInventory.Count > i)
                {
                    inventoryGrid[i].itemInThisGrid = inventory.ItemsInInventory[i];
                }
            }
		
            foreach (var grid in inventoryGrid)
            {
                grid.UpdateInfoOfPartOfGrid();
            }
        }

        // ReSharper disable once InconsistentNaming
        public void HelpPanelOnOff(bool onOff, Transform _Transform)
        {
            helpPanelInventory.SetActive(onOff);

            if (onOff)
            {
                helpPanelInventory.transform.position =
                    _Transform.position + new Vector3(80, 0, 0);
            }
        }
       
    }
}
