using System.Collections.Generic;
using ImportantScripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace ImportantScripts.NPCScripts
{
    public class Npc : MonoBehaviour
    {
        public NpcType TypeOfNpc = NpcType.Quest;

        public List<GameObject> ReqItemsForQuest;

        public string QuestText;
        public string QuestIsCompleteText;
        public string QuestIsNotCompleteText;

        public bool QuestComplete;
        private bool _inDialogue;

        public GameObject WhoIsTalking;
        public UnityEvent OnCompleteQuest;

        private string Name
        {
            get { return gameObject.name; }
        }

        public void Talk(GameObject whoIsTalking, int whatButtonPressed )
        {
            WhoIsTalking = whoIsTalking;
            _inDialogue = true;
            
            if (whatButtonPressed == -1)
            {
                CanvasManager.CanvasManagerIn.DialogueTextText.text = "Hello! My names is " + Name + ". What are you want?";
            }

            if (whatButtonPressed == 1)
            {
                QuestInteract(whoIsTalking);
            }

            if (whatButtonPressed == 2)
            {
                
            }
           
           
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Char>() != null)
            {
                _inDialogue = false;
                CanvasManager.CanvasManagerIn.DialogueTextText.text = "";
            }
        }

        private void Update()
        {
            if (!_inDialogue) return;
            if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
            
             if (TypeOfNpc == NpcType.Quest)
            {
                if (QuestComplete)
                {
                    QuestInteract(WhoIsTalking);  
                }
            }
        }

        public void QuestInteract(GameObject whoIsTalking)
        {
            CanvasManager.CanvasManagerIn.DialogueTextText.text = QuestText;
            if (!CheckInventory(whoIsTalking)) return;
            CanvasManager.CanvasManagerIn.DialogueTextText.text = QuestIsCompleteText;
            OnCompleteQuest.Invoke();
        }

        public int TradeInteract()
        {
            return 10;
        }
        
       
        
        private bool CheckInventory(GameObject whoIsTalking)
        {
            var matchItems = new List<GameObject>();
            var inventory = whoIsTalking.GetComponent<Char>().Inventory;

            foreach (var reqItem in ReqItemsForQuest)
            {
                foreach (var item in inventory)
                {
                    if (item != reqItem) continue;
                    matchItems.Add(item);
                    break;
                }
            }

            if (matchItems.Count != ReqItemsForQuest.Count)
                return false;

            foreach (var item in matchItems)
            {
                inventory.Remove(item);
            }

            return true;
        }
    }
}