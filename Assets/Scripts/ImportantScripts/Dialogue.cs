using System.Collections.Generic;
using ImportantScripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace ImportantScripts
{
	public class Dialogue : MonoBehaviour
	{
		public DialogueNode[] Node;
		public int CurrentNode;
		public bool ShowDialogue = true;
		public UnityEvent OnCompleteQuest;
		public List<GameObject> ReqItemsForQuest;
		public List<Answer> AnswersToShow; 
		public GameObject WhoIsTalking;
		
		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.GetComponent<Char>() == null) return;
			ShowDialogue = false;
			other.gameObject.GetComponent<Char>()._inDialogue = false;
		}

		public void UpdateNodeAndAnswers()
		{
			var currentNode = Node[CurrentNode];

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

		private void UpdateAnswersToShow()
		{
			AnswersToShow.Clear();

			foreach (var answer in Node[CurrentNode].PlayerAnswers)
			{
				if (answer.QuestCompleteAnswer)
				{
					if (CheckInventory(WhoIsTalking))
					{
						AnswersToShow.Add(answer);
					}
					
					continue;
				}

				AnswersToShow.Add(answer);
			}
		}
		
		private void Update()
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

			else
			{
				CanvasManager.CanvasManagerIn.DialogueTextNode.text = "";
				foreach (var t in CanvasManager.CanvasManagerIn.DialogueTextAnswer)
				{
					t.text = "";
				}
			}
		}


		private void IfSpecialAnswer(Answer playerAnswer)
		{
			if (playerAnswer.QuestCompleteAnswer)
			{
				if (CheckInventoryWithDelete(WhoIsTalking))
				{
					OnCompleteQuest.Invoke();
					WhoIsTalking.GetComponent<Char>()._inDialogue = false;
				}
			}
				
			if (playerAnswer.SpeakEnd)
			{
				ShowDialogue = false;
				WhoIsTalking.GetComponent<Char>()._inDialogue = false;
			}

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

			return matchItems.Count == ReqItemsForQuest.Count;
		}
		
		
		private bool CheckInventoryWithDelete(GameObject whoIsTalking)
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

	[System.Serializable]
	public class DialogueNode
	{
		public string NpcText;
		public List<Answer> PlayerAnswers;
	}
	
	[System.Serializable]
	public class Answer
	{
		public string Text;
		public int ToNode;
		public bool SpeakEnd;
		public bool QuestCompleteAnswer;
    }

	[System.Serializable]
	public class QuestAnswer : Answer
	{
		public List<GameObject> ReqItemsForQuest;
	}

}