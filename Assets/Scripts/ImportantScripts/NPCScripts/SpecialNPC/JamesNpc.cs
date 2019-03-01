using System.Collections.Generic;
using ImportantScripts.CharScripts;
using UnityEngine;

namespace ImportantScripts.NPCScripts.SpecialNPC
{
    public class JamesNpc : Dialogue
    {
        public Dialogue DialougeAfterQuestComplete;
        
        public List<Answer> NewAnswerAfterCompleteQuest;
           
           private void Start()
           {
               NewAnswerAfterCompleteQuest.Add(new Answer("Yes", 0, true, false));
           }
   
           public override void OnQuestComplete()
           {
               base.OnQuestComplete();
               Node.Add(new DialogueNode("And if you will need to fix the portal in the future, you can use my instruments. Here there are.",  NewAnswerAfterCompleteQuest));
               
               WhoIsTalking.GetComponent<Char>().IsInstrumentsPickedUp = true;
               
               UpdateNodeAndAnswers();
               UpdateAnswersToShow();
           }

        public override void OnDialogueStart(GameObject whoistalking)
        {
            base.OnDialogueStart(whoistalking);

            if (!IsQuestComplete) return;
            
            DialougeAfterQuestComplete.enabled = true;
            OnDialougeEnd();
            DialougeAfterQuestComplete.OnDialogueStart(whoistalking);
            enabled = false;
        }
    }  
}
