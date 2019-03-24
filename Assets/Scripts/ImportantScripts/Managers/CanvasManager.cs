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

		public void LootPanelOnOff(bool onoff, List<Item> itemsInfPanelOnOff)
		{
			lootPanel.SetActive(onoff);

			if (false)
			{
				UpdateLootPanel(itemsInfPanelOnOff);
			}
		}

		public void UpdateLootPanel(List<Item> items)
		{
			for (int i = 0; i < items.Count; i++)
			{
				LootGrid[i].GetComponent<InventoryGrid>().itemInThisGrid = items[i];
			}
		}

		public void SkillsPanelOnOff(bool onOff)
		{
			SkillsPanelSpace.SetActive(onOff);

			if (onOff)
			{
				FirstSkillElementToFocus.Select();
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				SkillsPanelOnOff(!SkillsPanelSpace.activeInHierarchy);
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

