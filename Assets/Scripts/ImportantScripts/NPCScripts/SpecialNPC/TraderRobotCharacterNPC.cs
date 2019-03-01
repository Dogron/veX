using System.Collections.Generic;
using UnityEngine;

namespace ImportantScripts.NPCScripts.SpecialNPC
{
    public class TraderRobotCharacterNpc : Dialogue
    {
        public Dialogue DialougeAfterQuestComplete;
        
        public List<Answer> NewAnswerAfterCompleteQuest;

        public override void OnQuestComplete()
        {
            base.OnQuestComplete();
            Node.Add(new DialogueNode("Thanks, that drone will help you with the... ERROR! REBOOT! REBOOT!",  NewAnswerAfterCompleteQuest));
            
        }


        public override void OnDialogueStart(GameObject whoistalking)
        {
            base.OnDialogueStart(whoistalking);
            if (IsQuestComplete)
            {
                DialougeAfterQuestComplete.enabled = true;
                OnDialougeEnd();
                DialougeAfterQuestComplete.OnDialogueStart(whoistalking);
                enabled = false;
            }
        }
    }
}