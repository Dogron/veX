using System.Collections.Generic;
using ImportantScripts.NPCScripts;

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
                if (Char.CharIn.Money - cost > item.MoneyCost)
                {
                   ItemsToReturn.Add(item);
                    cost += item.MoneyCost;
                }

                else
                {
                    break;
                }
            }

            Char.CharIn.Money -= cost;
           
            if (GetComponentInParent<TraderRobotNpc>())
            {
                var traderRobotNpc = GetComponentInParent<TraderRobotNpc>();
                traderRobotNpc.OnBuySomeThing();
            }
            
            return ItemsToReturn;
           
        }
    }
}