using ImportantScripts.CharScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ImportantScripts.Managers
{
	public class CanvasManager : MonoBehaviour
	{
	    public static CanvasManager CanvasManagerIn;
		
		public GameObject AmmoText;
		public GameObject HealthText;
		public GameObject DialogueText;

		public Text DialogueTextText;

		public Text DialogueTextNode;
		public Text[] DialogueTextAnswer;

		public GameObject InventorySpace;
		public GameObject[] InventoryGrid;
		public GameObject InfoButton;
		public GameObject InfoText;
		public GameObject TheGridWhatHaveBeenChosed;
		public Selectable FirstElementToFocus;
		
		private void Awake()
		{
			CanvasManagerIn = this;

		}

		private void Start()
		{
			InventoryOnOff(false);
			Cursor.visible = false;
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

		private void Update()
		{
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
			
			if (Input.GetKeyDown(KeyCode.I))
			{
				var isActive = InventorySpace.activeInHierarchy;
				InventoryOnOff(!isActive);
           
				if (InventorySpace.activeInHierarchy)
				{
					UpdateInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>());
				}
			}

			if (Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				if (EventSystem.current.currentSelectedGameObject.GetComponent<InventoryGrid>() != null)
				{
					TheGridWhatHaveBeenChosed = EventSystem.current.currentSelectedGameObject;
				}
			}

			if (Input.GetKeyDown(KeyCode.K))
			{
				InfoText.GetComponent<Text>().text = TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid != null ? TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid.InfoAbout : "";
			}

			if (Input.GetKeyDown(KeyCode.L))
			{
				if (TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid != null)
				{
					TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid.OnUse();
				}
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				TheGridWhatHaveBeenChosed.GetComponent<InventoryGrid>().ItemInThisGrid.Amount -= 1;
			}
		}

		public void UpdateInventory(Inventory inventory)
		{
			for (var i = 0; i < InventoryGrid.Length; i++)
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
