using UnityEngine;

namespace ImportantScripts.Interactables
{
	public class BushWithBerries : MonoBehaviour
	{

		public TypesOfBerries TypeOfBerries;
		public int StateOfBerries;

		private void Start()
		{
			StateOfBerries = 2;
			UpdateTypeOfBerries();
		}


		void UpdateTypeOfBerries ()
		{
			var i = Random.Range(0, 1);
			TypeOfBerries = i == 1 ? TypesOfBerries.Heal : TypesOfBerries.Poison;
		}
	
		void Update () {
		    
		}
	}
}
