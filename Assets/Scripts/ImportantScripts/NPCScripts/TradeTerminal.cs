using System.Collections.Generic;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using ImportantScripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ImportantScripts.NPCScripts
{
    public class TradeTerminal : MonoBehaviour
    {
        public List<Item> WhoIsTradingItems;
        public List<Item> TraderItems;
       
        public GameObject WhoIsTrading;

        public TradeManager TraderItemsMan;

        public GameObject GridWhatHaveBeenChosed;
        private void Start()
        {
            TraderItemsMan = TradeManager.TradeManagerIn;
        }


        public void StartTrade(GameObject whoistrading)
        {
            TradeManager.TradeManagerIn.TraderPanelOnOff(true);
            WhoIsTrading = whoistrading;
            WhoIsTradingItems = WhoIsTrading.gameObject.GetComponent<Inventory>().ItemsInInventory;
            UpdateTradePanel();
        }

        public void UpdateTradePanel()
        {
            foreach (var grid in TraderItemsMan.TraderGridsTradePanel)
            {
                    grid.gameObject.GetComponentInChildren<Text>().text = "";
                    grid.GetComponent<InventoryGrid>().itemInThisGrid = null;
            }
            
            foreach (var grid in TraderItemsMan.InventoryGridsTradePanel)
            {
                grid.gameObject.GetComponentInChildren<Text>().text = "";
                grid.GetComponent<InventoryGrid>().itemInThisGrid = null;
            }

            for (int i = 0; i < WhoIsTradingItems.Count; i++)
            {
                TraderItemsMan.InventoryGridsTradePanel[i].itemInThisGrid = WhoIsTradingItems[i];
            }

            for (int i = 0; i < TraderItems.Count; i++)
            {
                TraderItemsMan.TraderGridsTradePanel[i].itemInThisGrid = TraderItems[i];     
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                WhoIsTrading = null;
            }
            
            if (WhoIsTrading != null)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    GridWhatHaveBeenChosed = EventSystem.current.currentSelectedGameObject;
                    
                    TraderItemsMan.HelpPanelOnOff(true,GridWhatHaveBeenChosed.transform.position + new Vector3(20,0,0));
                }

                if (TraderItemsMan.HelpPanel.activeInHierarchy)
                {
                    if (Input.GetKeyDown(KeyCode.L))
                    {
                        if (GridWhatHaveBeenChosed.GetComponent<InventoryGrid>() != null)
                        {
                            TraderItemsMan.HelpPanelOnOff(false,new Vector3(0,0,0));
                            
                            var item = GridWhatHaveBeenChosed.GetComponent<InventoryGrid>()
                                .itemInThisGrid;
                            var Char = WhoIsTrading.gameObject.GetComponent<Char>();
                            var inventory = WhoIsTrading.gameObject.GetComponent<Inventory>();
                            
                            if (GridWhatHaveBeenChosed.GetComponentInParent<GridLayoutGroup>().gameObject.name == TraderItemsMan.InventoryTradePanel.name)
                            {
                                Char.Money += item.moneyCost;
                                TraderItems.Add(new Item(item.itemGameObject,item.amountOfItem,item.infoAbout,item.amountOfResource,item.isUseble,item.moneyCost,item.ItemType,item.IsItNoStaking,item.Name));
                                inventory.RemoveFromInventory(item);
                            }

                            if (GridWhatHaveBeenChosed.GetComponentInParent<GridLayoutGroup>().gameObject.name == TraderItemsMan.TraderTradePanel.name)
                            {
                                if (Char.Money >= item.moneyCost)
                                {
                                    Char.Money -= item.moneyCost;
                                    inventory.AddToInventory(new Item(item.itemGameObject,item.amountOfItem,item.infoAbout,item.amountOfResource,item.isUseble,item.moneyCost,item.ItemType,item.IsItNoStaking,item.Name));
                                    TraderItems.Remove(item);
                                }
                            } 
                        }
                    }
                    
                    if (GridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid != null)
                    {
                        TraderItemsMan.MoneyText.GetComponent<Text>().text = GridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid.moneyCost.ToString();
                    }

                    else
                    {
                        TraderItemsMan.MoneyText.GetComponent<Text>().text = "";
                    }
                }
                
                UpdateTradePanel(); 
            }
        }
    }
}