using System.Collections.Generic;
using ImportantScripts.CharScripts;
using ImportantScripts.NPCScripts;
using ImportantScripts.NPCScripts.SpecialNPC;

namespace ImportantScripts.ItemsScripts
{
    public class TradeItemProvider : ItemProvider
    {
        public List<Item> ItemsToReturn;
        
        public override List<Item> Consume()
        {
            var cost = 0;
            ItemsToReturn.Clear();

            foreach (var item in ItemsInProvider)
            {
                if (Char.CharIn.Money - cost > item.moneyCost)
                {
                   ItemsToReturn.Add(item);
                    cost += item.moneyCost;
                }

                else
                {
                    break;
                }
            }

            Char.CharIn.Money -= cost;
            return ItemsToReturn;
           
        }
    }
}