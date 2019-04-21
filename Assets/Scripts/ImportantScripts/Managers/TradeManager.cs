using System.Collections.Generic;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using UnityEngine;
using UnityEngine.UI;

namespace ImportantScripts.Managers
{
    public class TradeManager : MonoBehaviour
    {
        public static TradeManager TradeManagerIn;

        public GameObject TradePanel;
        public Selectable FirstButtonToSelect;
       
        public GameObject InventoryTradePanel;
        public List<InventoryGrid> InventoryGridsTradePanel;
        public GameObject TraderTradePanel;
        public List<InventoryGrid> TraderGridsTradePanel;

        public GameObject HelpPanel;
        public GameObject MoneyText;
        
        private void Awake()
        {
            TradeManagerIn = this;
        }

        public void HelpPanelOnOff(bool onoff,Vector3 _transform)
        {
            HelpPanel.SetActive(onoff);
            if (onoff)
            {
                HelpPanel.transform.position = _transform;
            }
        }
        public void TraderPanelOnOff(bool onoff)
        {
            TradePanel.SetActive(onoff);

            if (onoff)
            {
                FirstButtonToSelect.Select();
            }
        }
    }
}