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
        private void Awake()
        {
            TradeManagerIn = this;
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