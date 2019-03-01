﻿using System.Collections.Generic;
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

		public GameObject DialougePanel;
		
		public GameObject AmmoText;
		public GameObject HealthText;

		public Text DialogueTextNode;
		public Text[] DialogueTextAnswer;

		public GameObject InventorySpace;
		public List<GameObject> InventoryGrid;
		public GameObject InfoText;
		public GameObject TheGridWhatHaveBeenChosed;
		public Selectable FirstElementToFocus;
		public GameObject HelpPanelInventory;

		public GameObject SkillsPanelSpace;
		public GameObject TheSkillWhatHaveBeenChosed;
		public Selectable FirstSkillElementToFocus;
		public GameObject CurrentAmountOfSkillPoints;

		
		
		public GameObject XpText;
		
		
		private void Awake()
		{
			CanvasManagerIn = this;
		}

		private void Start()
		{
			InventoryOnOff(false);
			Cursor.visible = false;

			var listofinventorygrid = InventorySpace.GetComponentsInChildren<InventoryGrid>();
			foreach (var invgrid in listofinventorygrid)
			{
				InventoryGrid.Add(invgrid.gameObject);
			}
		}

		public void InventoryOnOff(bool onOff)
		{
			InventorySpace.SetActive(onOff);
			InfoText.GetComponent<Text>().text = "";

			if (onOff)
			{
				FirstElementToFocus.Select();
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
				InventoryOnOff(false);
			}

			if (Input.GetKeyDown(KeyCode.I))
			{
				var isActive = InventorySpace.activeInHierarchy;
				InventoryOnOff(!isActive);
           
				if (InventorySpace.activeInHierarchy)
				{
					UpdateInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>());
				}
				
				SkillsPanelOnOff(false);
			}
			
			
			if (SkillsPanelSpace.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.C))
				{
					if (EventSystem.current.currentSelectedGameObject.GetComponent<SkillsPanelGrid>() != null)
					{
						Char.CharIn.StatsUp(EventSystem.current.currentSelectedGameObject.GetComponent<SkillsPanelGrid>().WhatSkillIsThat);
					}
				}

				CurrentAmountOfSkillPoints.GetComponent<Text>().text = "Amount of skill points : " + Char.CharIn.CurrAmountOfSkillPoints.ToString();
			}
			
			var weapon = Char.CharIn.CurrentWeapon;
			
			if (weapon == null)
			{
				AmmoText.GetComponent<Text>().text = "No weapon";
			}
			else
			{
				AmmoText.GetComponent<Text>().text = weapon.Name + " ammo: " + weapon.AmmoNow + " of " + weapon.AmmoMax;
			}

			HealthText.GetComponent<Text>().text = Char.CharIn.HpNow + "/" + Char.CharIn.MaxHp + "   HP";

			XpText.GetComponent<Text>().text = Char.CharIn.Xp + "/" + Char.CharIn.XpReqForNewLvl + "   xp";
			
			
			if (InventorySpace.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.C))
				{
					print("Enter has pressed");
				
					if (EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>() != null)
					{
						print("The Grid has been Chosed");
					
						TheGridWhatHaveBeenChosed = EventSystem.current.currentSelectedGameObject;

						HelpPanelInventory.SetActive(true);
						HelpPanelInventory.transform.position =
							TheGridWhatHaveBeenChosed.transform.position + new Vector3(80, 0, 0);

					}
				}

				if (Input.GetKeyDown(KeyCode.K))
				{
					print("K has pressed");
					
					InfoText.GetComponent<Text>().text = TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid != null ? TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid.InfoAbout : "";
					
					HelpPanelInventory.SetActive(false);
				}

				if (Input.GetKeyDown(KeyCode.L))
				{
					if (TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid != null)
					{
						Char.CharIn.OnUseItem(TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid);
					}
					
					HelpPanelInventory.SetActive(false);
				}

				if (Input.GetKeyDown(KeyCode.R))
				{
					TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid.AmountOfItem -= 1;
					
					HelpPanelInventory.SetActive(false);
				}
			}
			
		}

		public void UpdateInventory(Inventory inventory)
		{
			for (var i = 0; i < InventoryGrid.Count; i++)
			{
			InventoryGrid[i].GetComponent<InventoryGrid>().ItemInThisGrid = inventory.ItemsInInventory.Count > i ? inventory.ItemsInInventory[i] : null;
			}
		
			foreach (var grid in InventoryGrid)
			{
				grid.GetComponent<InventoryGrid>().UpdateInfoOfPartOfGrid();
			}
		}
	}
}
