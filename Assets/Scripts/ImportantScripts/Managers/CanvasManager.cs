using System.Collections.Generic;
using System.Linq;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ImportantScripts.Managers
{
	public class CanvasManager : MonoBehaviour
	{
		public static CanvasManager CanvasManagerIn;

		public GameObject dialougePanel;

		public GameObject ammoText;
		public GameObject healthText;
		public GameObject moneyText;

		public Text dialogueTextNode;
		public Text[] dialogueTextAnswer;

		public GameObject inventorySpace;
		public List<GameObject> InventoryGrid;

		public GameObject SkillsPanelSpace;
		public Selectable FirstSkillElementToFocus;
		public GameObject CurrentAmountOfSkillPoints;

		public GameObject XpText;

		public GameObject lootPanel;
		public List<GameObject> LootGrid;
		public Selectable FirstLootGridToFocus;
		public InventoryGrid GridWhatHaveBeenChosed;
		public Text InfoLootText;
		public ItemProvider CurrentProvider;
		private void Awake()
		{
			CanvasManagerIn = this;
		}

		private void Start()
		{
			Cursor.visible = false;

			var listofinventorygrid = inventorySpace.GetComponentsInChildren<InventoryGrid>();
			foreach (var invgrid in listofinventorygrid)
			{
				InventoryGrid.Add(invgrid.gameObject);
			}
		}

		public void LootPanelOnOff(bool onoff, ItemProvider provider)
		{
			CloseEverything();
			
			lootPanel.SetActive(onoff);

			if (onoff)
			{
				CurrentProvider = provider;
				FirstLootGridToFocus.Select();
				UpdateLootPanel(provider);
			}

			else
			{
				CurrentProvider = null;
			}
		}

		private void UpdateLootPanel(ItemProvider items)
		{
			foreach (var grid in LootGrid)
			{
				grid.gameObject.GetComponentInChildren<Text>().text = "";
				grid.GetComponent<InventoryGrid>().itemInThisGrid = null;
			}
			
			for (int i = 0; i < items.Consume().Count; i++)
			{
				LootGrid[i].GetComponent<InventoryGrid>().itemInThisGrid = items.Consume()[i];
			}
		}

		private void SkillsPanelOnOff(bool onOff)
		{
			CloseEverything();
			SkillsPanelSpace.SetActive(onOff);

			if (onOff)
			{
				FirstSkillElementToFocus.Select();
			}
		}

		public void CloseEverything()
		{
			lootPanel.SetActive(false);
			SkillsPanelSpace.SetActive(false);
			inventorySpace.SetActive(false);
			TradeManager.TradeManagerIn.TradePanel.SetActive(false);
		}
		
		
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				CloseEverything();
			}
			
			
			if (Input.GetKeyDown(KeyCode.P))
			{
				SkillsPanelOnOff(!SkillsPanelSpace.activeInHierarchy);
			}

			if (lootPanel.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.C))
				{
					if (EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>() != null)
					{
						GridWhatHaveBeenChosed =
							EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>();
						UpdateLootPanel(CurrentProvider);
					}
				}

				if (Input.GetKeyDown(KeyCode.K))
				{
					  InfoLootText.GetComponent<Text>().text = GridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid != null ? GridWhatHaveBeenChosed.GetComponent<InventoryGrid>().itemInThisGrid.infoAbout : "";
					  UpdateLootPanel(CurrentProvider);
				}

				if (Input.GetKeyDown(KeyCode.L))
				{
					Char.CharIn.AddToInventoryChar(CurrentProvider,EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>().itemInThisGrid);
					
					UpdateLootPanel(CurrentProvider);
				}

				if (CurrentProvider.ItemsInProvider.Count == 0)
				{
					LootPanelOnOff(false,null);
				}
			}
			
			if (SkillsPanelSpace.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.C))
				{
					if (EventSystem.current.currentSelectedGameObject.GetComponent<SkillsPanelGrid>() != null)
					{
						Char.CharIn.StatsUp(EventSystem.current.currentSelectedGameObject
							.GetComponent<SkillsPanelGrid>().WhatSkillIsThat);
					}
				}

				CurrentAmountOfSkillPoints.GetComponent<Text>().text =
					"Amount of skill points : " + Char.CharIn.CurrAmountOfSkillPoints.ToString();
			}

			var weapon = Char.CharIn.CurrentWeapon;

			if (weapon == null)
			{
				ammoText.GetComponent<Text>().text = "No weapon";
			}
			else
			{
				ammoText.GetComponent<Text>().text = weapon.Name + " ammo: " + weapon.AmmoNow + " of " + weapon.AmmoMax;
			}

			healthText.GetComponent<Text>().text = Char.CharIn.HpNow + "/" + Char.CharIn.MaxHp + "   HP";

			XpText.GetComponent<Text>().text = Char.CharIn.Xp + "/" + Char.CharIn.XpReqForNewLvl + "   xp";


			if (inventorySpace.activeInHierarchy)
			{
				moneyText.GetComponent<Text>().text = Char.CharIn.Money + " Money";
			}
		}
	}
}	

