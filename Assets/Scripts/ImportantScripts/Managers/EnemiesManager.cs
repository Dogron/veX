using System.Collections;
using System.Collections.Generic;
using ImportantScripts.ItemsScripts;
using ImportantScripts.NPCScripts;
using ImportantScripts.NPCScripts.EnemiesScripts;
using UnityEngine;

namespace ImportantScripts.Managers
{
	public class EnemiesManager : MonoBehaviour
	{

		public GameObject Enemy;

		public static EnemiesManager EnemiesManagerIn;
		
		public GameObject[] EnemySpawns;

		public List<GameObject> Enemies;
		
		public List<Item> AllLoot;

		private void Awake()
		{
			EnemiesManagerIn = this;
		}

		void Start ()
		{
			StartCoroutine(SpawnEnemes());
		}
		
		private IEnumerator SpawnEnemes()
		{
			if (Enemies.Count < 12)
			{
				var enemy = Instantiate(Enemy);
				Enemies.Add(enemy);
				enemy.gameObject.transform.position = EnemySpawns[Random.Range(0, EnemySpawns.Length)].gameObject.transform.position;
				var enemyc = enemy.GetComponent<Enemy>();
				enemyc.ChooseHowManyLootOnEnemy();
				enemyc.GetComponent<Enemy>().SetLootOnEnemy(AllLoot);
			}	
			yield return new WaitForSeconds(6);
			StartCoroutine(SpawnEnemes());
		}
	}
}
