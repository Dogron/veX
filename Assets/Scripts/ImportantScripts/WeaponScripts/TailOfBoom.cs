using System.Collections;
using UnityEngine;

namespace ImportantScripts
{
	public class TailOfBoom : MonoBehaviour {

	
		void Start ()
		{
			StartCoroutine(Destroy());
		}

		IEnumerator Destroy()
		{
			yield return new WaitForSeconds(0.3f);
			Destroy(gameObject);
		}
	}
}
	