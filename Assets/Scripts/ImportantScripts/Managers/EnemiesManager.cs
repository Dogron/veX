using System.Collections;
using UnityEngine;

namespace ImportantScripts.Managers
{
	public class EnemiesManager : MonoBehaviour
	{

		public GameObject Enemy;

		public GameObject[] EnemySpawns;
	
		void Start ()
		{
			StartCoroutine(SpawnEnemes());
		}
		
		private IEnumerator SpawnEnemes()
		{
			Instantiate(Enemy).gameObject.transform.position = EnemySpawns[Random.Range(0, EnemySpawns.Length)].gameObject.transform.position;
			yield return new WaitForSeconds(6);
			StartCoroutine(SpawnEnemes());
		}
	}
}
