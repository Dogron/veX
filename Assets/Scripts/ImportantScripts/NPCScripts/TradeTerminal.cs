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
        public GameObject InventoryTradePanel;
        public List<InventoryGrid> InventoryGridsTradePanel;
        public GameObject TraderTradePanel;
        public List<InventoryGrid> TraderGridsTradePanel;

        public GameObject WhoIsTrading;
        
        public void StartTrade(GameObject whoistrading)
        {
            TradeManager.TradeManagerIn.TraderPanelOnOff(true);
            WhoIsTrading = whoistrading;
            WhoIsTradingItems = WhoIsTrading.gameObject.GetComponent<Inventory>().ItemsInInventory;
            UpdateTradePanel();
        }

       
        
        public void UpdateTradePanel()
        {
            foreach (var grid in TraderGridsTradePanel)
            {
                    grid.gameObject.GetComponentInChildren<Text>().text = "";
                    grid.GetComponent<InventoryGrid>().itemInThisGrid = null;
            }
            
            foreach (var grid in InventoryGridsTradePanel)
            {
                grid.gameObject.GetComponentInChildren<Text>().text = "";
                grid.GetComponent<InventoryGrid>().itemInThisGrid = null;
            }

            for (int i = 0; i < WhoIsTradingItems.Count; i++)
            {
                InventoryGridsTradePanel[i].itemInThisGrid = WhoIsTradingItems[i];
            }

            for (int i = 0; i < TraderItems.Count; i++)
            {
                TraderGridsTradePanel[i].itemInThisGrid = TraderItems[i];     
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
                    if (EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>() != null)
                    {
                        var item = EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>()
                            .itemInThisGrid;
                        var Char = WhoIsTrading.gameObject.GetComponent<Char>();
                        var inventory = WhoIsTrading.gameObject.GetComponent<Inventory>();
                        if (EventSystem.current.currentSelectedGameObject.GetComponentInParent<GridLayoutGroup>().gameObject.name == InventoryTradePanel.name)
                        {
                            Char.Money += item.moneyCost;
                            TraderItems.Add(new Item(item.itemGameObject,item.amountOfItem,item.infoAbout,item.amountOfResource,item.isUseble,item.moneyCost,item.ItemType,item.IsItNoStaking,item.Name));
                            inventory.RemoveFromInventory(item);
                        }

                        if (EventSystem.current.currentSelectedGameObject.GetComponentInParent<GridLayoutGroup>().gameObject.name == TraderTradePanel.name)
                        {
                            if (Char.Money >= item.moneyCost)
                            {
                                Char.Money -= item.moneyCost;
                                inventory.AddToInventory(new Item(item.itemGameObject,item.amountOfItem,item.infoAbout,item.amountOfResource,item.isUseble,item.moneyCost,item.ItemType,item.IsItNoStaking,item.Name));
                                TraderItems.Remove(item);
                            }
                        }
                    }
               
                    UpdateTradePanel();
                }
            }
        }
    }
}