using ImportantScripts.CharScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ImportantScripts.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager InventoryManagerIn;

        public GameObject InventorySpace;
        public GameObject[] InventoryGrid;

        public Selectable firstElementToSelect;
        
        public GameObject InfoText;
        public GameObject theGridWhatHaveBeenChosed;

        public GameObject HelpPanelInventory;
        
        public CanvasManager canvasManager = CanvasManager.CanvasManagerIn;
        
        private void Awake()
        {
            InventoryManagerIn = this;
        }
       
        public void InventoryOnOff(bool onoff)
        {
            CanvasManager.CanvasManagerIn.CloseEverything();
            InventorySpace.SetActive(onoff);
            InfoText.GetComponent<Text>().text = "";

            if (InventorySpace.activeInHierarchy)
            {
                firstElementToSelect.Select();
                UpdateInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>());
            }
        }

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                var isActive = InventorySpace.activeInHierarchy;
                InventoryOnOff(!isActive);
            }

            if (CanvasManager.CanvasManagerIn.inventorySpace.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
	                print("Enter has pressed");
                					
	                if (EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>() != null)
	                {
		                print("The Grid has been Chosed");
		                theGridWhatHaveBeenChosed = EventSystem.current.currentSelectedGameObject;
		                HelpPanelOnOff(true,theGridWhatHaveBeenChosed.transform);
	                }
                }
               
               
                
                if (Input.GetKeyDown(KeyCode.K))
                {
	                print("K has pressed");
	                InfoText.GetComponent<Text>().text = theGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid != null ? theGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid.infoAbout : "";
                }
                				
                if (Input.GetKeyDown(KeyCode.L))
                {
	                if (theGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid != null)
	                {
		                Char.CharIn.OnUseItem(theGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid);
	                }
	                HelpPanelOnOff(false,null);
                }
                				
                if (Input.GetKeyDown(KeyCode.R))
                {
	                theGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid.amountOfItem -= 1;
	                HelpPanelOnOff(false,null);
                }
            }
        }

        public void UpdateInventory(Inventory inventory)
        {
            for (var i = 0; i < InventoryGrid.Length; i++)
            {
                if (inventory.ItemsInInventory.Count > i)
                {
                    InventoryGrid[i].GetComponent<InventoryGrid>().itemInThisGrid = inventory.ItemsInInventory[i];
                }
            }
		
            foreach (var grid in InventoryGrid)
            {
                grid.GetComponent<InventoryGrid>().UpdateInfoOfPartOfGrid();
            }
        }

        public void HelpPanelOnOff(bool onOff, Transform Transform)
        {
            HelpPanelInventory.SetActive(onOff);

            if (onOff)
            {
                HelpPanelInventory.transform.position =
                    Transform.position + new Vector3(80, 0, 0);
            }
        }
       
    }
}
