using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ImportantScripts.Interactables
{
	public class BushWithBerries : MonoBehaviour
	{
		public TypesOfBerries TypeOfBerries;
		public string StateOfBerries = "grown";
		public GameObject[] Berries;
		
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
			var i = Random.Range(0, 2);
			
			switch (i)
			{
				case 0:
					TypeOfBerries = TypesOfBerries.Poison;
					break;
				case 1:
					TypeOfBerries = TypesOfBerries.Heal;
					break;
			}

			print(TypeOfBerries);
		}

		public int Collect()
		{
			if (StateOfBerries != "grown") return 0;
			
			StateOfBerries = "growing";
			
			var amount = GetComponent<NotExpendableResourceProvider>().Amount;
			
			foreach (var berry in Berries)
			{
				berry.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2,
					transform.localScale.z / 2);
			}

			StartCoroutine(GrowingCoroutine());
			
			switch (TypeOfBerries)
			{
				case TypesOfBerries.Heal:
					return amount;
				case TypesOfBerries.Poison:
				    return -amount;
				default:
					throw new ArgumentOutOfRangeException();
			}

		}
		
		
		public IEnumerator GrowingCoroutine()
		{
			yield return new WaitForSeconds(20);
			StateOfBerries = "grown";
			foreach (var berry in Berries)
			{
				berry.transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y * 2,
				transform.localScale.z * 2);
			}
			
			UpdateTypeOfBerries();
		}
	}
}
