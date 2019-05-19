
using UnityEngine;

namespace ImportantScripts.Managers
{
	public class GameManager : MonoBehaviour
	{

		public static GameManager GameManagerIn;

		public GameObject Char;
		public GameObject Camera;
		public GameObject Spawn;
		
		private void Awake()
		{
			GameManagerIn = this;
		}

		private void Start()
		{
			Char.transform.position = Spawn.transform.position + new Vector3(0,1,0);
		}
		
	}
}
