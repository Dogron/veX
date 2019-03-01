using System.Collections.Generic;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using ImportantScripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace ImportantScripts.NPCScripts
{
	public class Dialogue : MonoBehaviour
	{
		public List<DialogueNode> Node;
		public int CurrentNode;
		public bool ShowDialogue;
		public UnityEvent OnCompleteQuest;
		public List<Item> ReqItemsForQuest;
		public List<Answer> AnswersToShow;
		public GameObject WhoIsTalking;
		// ReSharper disable once InconsistentNaming
		public DialogueNode currentNode;
		public bool IsQuestComplete;
		
		
		public virtual void OnDialogueStart(GameObject whoistalking)
		{
			WhoIsTalking = whoistalking;
			ShowDialogue = true;
			CanvasManager.CanvasManagerIn.DialougePanel.SetActive(true);
		}
		

		public void OnDialougeEnd()
		{
			CanvasManager.CanvasManagerIn.DialogueTextNode.text = "";
		    foreach (var t in CanvasManager.CanvasManagerIn.DialogueTextAnswer)
			{
				t.text = "";
			}
			ShowDialogue = false;
			WhoIsTalking.GetComponent<Char>().InDialogue = false;
			CanvasManager.CanvasManagerIn.DialougePanel.SetActive(false);
		}
		public virtual void OnQuestComplete()
		{
			IsQuestComplete = true;
			OnCompleteQuest.Invoke();
		}
		
		public void UpdateNodeAndAnswers()
		{
			currentNode= Node[CurrentNode];

			CanvasManager.CanvasManagerIn.DialogueTextNode.text = currentNode.NpcText;

			int i;

			for (i = 0; i < AnswersToShow.Count; i++)
			{
				CanvasManager.CanvasManagerIn.DialogueTextAnswer[i].text = AnswersToShow[i].Text;
			}

			for (; i < CanvasManager.CanvasManagerIn.DialogueTextAnswer.Length; i++)
			{
				CanvasManager.CanvasManagerIn.DialogueTextAnswer[i].text = "";
			}
		}

		public void UpdateAnswersToShow()
		{
			AnswersToShow.Clear();

			foreach (var answer in Node[CurrentNode].PlayerAnswers)
			{
				if (answer.QuestCompleteAnswer)
				{
					if (CheckInventory(WhoIsTalking, false))
					{
						AnswersToShow.Add(answer);
					}

					continue;
				}

				AnswersToShow.Add(answer);
			}
		}

		public void Update()
		{
			if (ShowDialogue)
			{
				UpdateAnswersToShow();
				UpdateNodeAndAnswers();

				if (!Input.anyKeyDown) return;

				var ch = Input.inputString;
				int n;

				if (!int.TryParse(ch, out n)) return;
				n = n - 1;

				if (n >= AnswersToShow.Count) return;

				var playerAnswer = AnswersToShow[n];

				IfSpecialAnswer(playerAnswer);
				CurrentNode = playerAnswer.ToNode;

			}

			
		}


		public void IfSpecialAnswer(Answer playerAnswer)
		{
			if (playerAnswer.QuestCompleteAnswer)
			{
				if (CheckInventory(WhoIsTalking, true))
				{
					OnQuestComplete();
				}
			}

			if (playerAnswer.SpeakEnd)
			{
				OnDialougeEnd();
			}
		}

		
		
		public bool CheckInventory(GameObject whoIsTalking, bool withDelete)
		{
			var matchItems = new List<Item>();

			var inventory = whoIsTalking.GetComponent<Inventory>();

			foreach (var reqItem in ReqItemsForQuest)
			{
				foreach (var item in inventory.ItemsInInventory)
				{
					if (item.ItemGameObject != reqItem.ItemGameObject) continue;
					if (item.AmountOfItem < reqItem.AmountOfItem) continue;
 
					matchItems.Add(item);
					break;
				}
			}
			print(matchItems.Count);
			
			if (withDelete && matchItems.Count == ReqItemsForQuest.Count)
			{
				print("SucsessCheckInventory");
				
				foreach (var reqItem in ReqItemsForQuest)
				{
					foreach (var matchItem in matchItems)
					{
						if (matchItem.ItemGameObject == reqItem.ItemGameObject)
						{
							matchItem.AmountOfItem -= reqItem.AmountOfItem;
						}
					}
				}
			}

			return matchItems.Count == ReqItemsForQuest.Count;
		}
	}


[System.Serializable]
	public class DialogueNode
	{
		public string NpcText;
		public List<Answer> PlayerAnswers;

		public DialogueNode(string npcText, List<Answer> playerAnswers)
		{
			NpcText = npcText;
			PlayerAnswers = playerAnswers;
		}
	}
	
	[System.Serializable]
	public class Answer
	{
		public string Text;
		public int ToNode;
		public bool SpeakEnd;
		public bool QuestCompleteAnswer;

		public Answer(string text, int toNode, bool speakEnd, bool questCompleteAnswer)
		{
			Text = text;
			ToNode = toNode;
			SpeakEnd = speakEnd;
			QuestCompleteAnswer = questCompleteAnswer;
		}
	}

	[System.Serializable]
	public class QuestAnswer : Answer
	{
		public QuestAnswer(string text, int toNode, bool speakEnd, bool questCompleteAnswer) : base(text, toNode, speakEnd, questCompleteAnswer)
		{
		}
	}

}