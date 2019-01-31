using System.Collections.Generic;
using UnityEngine;

namespace ImportantScripts.ItemsScripts
{
    public abstract class ItemProvider : MonoBehaviour
    {
        public List<Item> ItemsInProvider;
        public abstract List<Item> Consume();
    }

}
