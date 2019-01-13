using ImportantScripts.Managers;
using UnityEngine;

namespace ImportantScripts.Interactables
{
	public class Stair : Interactable {

		void Update ()
		{
			if (!PlayerNearby) return;
			if (Input.GetKey(KeyCode.E))
			{
				GameManager.GameManagerIn.Char.transform.position += new Vector3(0, 0.1f, 0);	
			}
		}

	}
}
