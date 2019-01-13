using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ImportantScripts.Interactables
{
	public class BushWithBerries : MonoBehaviour
	{
		public TypesOfBerries TypeOfBerries;
		public string StateOfBerries = "grown";
		
		//public readonly ObservableProperty<string> StateOfBerries = new ObservableProperty<string>("grown");
		
		private void Start()
		{
			UpdateTypeOfBerries();
			
			/*StateOfBerries.PropertyChanged += (sender, args) =>
			{
				
			};
		    */
		}


		void UpdateTypeOfBerries ()
		{
			var i = Random.Range(0, 1);
			TypeOfBerries = i == 1 ? TypesOfBerries.Heal : TypesOfBerries.Poison;
		}
	
		void Update () {
			if (StateOfBerries == "grown")
			{
				
			}
		}
	}
}
