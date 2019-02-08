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
            Node.Add(new DialogueNode("And if you will need to fix the portal in the future, you can use my instruments. Here there are.",  NewAnswerAfterCompleteQuest));
        }


        public override void OnDialogueStart(GameObject whoistalking)
        {
            base.OnDialogueStart(whoistalking);
            if (IsQuestComplete)
            {
                DialougeAfterQuestComplete.enabled = true;
                DialougeAfterQuestComplete.OnDialogueStart(whoistalking);
                OnDialougeEnd();
                enabled = false;
            }
        }
    }
}