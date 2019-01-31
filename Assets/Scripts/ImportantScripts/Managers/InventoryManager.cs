using ImportantScripts.CharScripts;
using UnityEngine;
using UnityEngine.UI;

namespace ImportantScripts.Managers
{
    public class InventoryManager : MonoBehaviour
    {

        public static InventoryManager InventoryManagerIn;

        public GameObject InventorySpace;
        public GameObject[] InventoryGrid;
        public GameObject InfoButton;
        public GameObject InfoText;
        public GameObject TheGridWhatHaveBeenChosed;
		
        private void Awake()
        {
            InventoryManagerIn = this;
        }

        private void Start()
        {
            InventoryOnOff(false);
        }

        public void InventoryOnOff(bool onoff)
        {
            InventorySpace.SetActive(onoff);
            InfoText.GetComponent<Text>().text = "";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                var isActive = InventorySpace.activeInHierarchy;
                InventoryOnOff(!isActive);
           
                if (InventorySpace.activeInHierarchy)
                {
                    UpdateInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>());
                }
            }
        }

        public void UpdateInventory(Inventory inventory)
        {
            for (var i = 0; i < InventoryGrid.Length; i++)
            {
                if (inventory.ItemsInInventory.Count > i)
                {
                    InventoryGrid[i].GetComponent<InventoryGrid>().ItemInThisGrid = inventory.ItemsInInventory[i];
                }
            }
		
            foreach (var grid in InventoryGrid)
            {
                grid.GetComponent<InventoryGrid>().UpdateInfoOfPartOfGrid();
            }
        }

        public void OnButtonInfoPressed()
        {
            InfoText.GetComponent<Text>().text = TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid != null ? TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid.InfoAbout : "";
        }

        public void OnButtonUsePressed()
        {
            if (TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid != null)
            {
                TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid.OnUse();
            }
        }
    }
}
