using UnityEngine;
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

		void Awake()
		{
			CanvasManagerIn = this;
		}
		
	
		void Update () {
			
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
		}
	}
}
