using System.Collections;
using System.Collections.Generic;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using ImportantScripts.Managers;
using ResourcesAndItems;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace ImportantScripts.NPCScripts.EnemiesScripts
{
	public class Enemy : MonoBehaviour
	{
		public NavMeshAgent Agent;

		public int Hp;

		private bool _canAttack = true;

		public EnemyStates EnemyState;

		public SphereCollider SphereCollider;
		
		public int Speed;
		
		public int AttackPower;
		public int ReloadSpeed;

		public List<Item> LootOnEnemy;

		public GameObject LootPocket;
		
		public int HowManyLootOnEnemy;

		private void Start()
		{
			ChangeStatesFun(EnemyStates.Idle,null);
		}

		public void ChooseHowManyLootOnEnemy()
		{
			HowManyLootOnEnemy = Random.Range(1, 4);
		}

		public void Damage(int damage)
		{
			Hp -= damage;

			if (Hp > 0) return;
			
			DropLoot();
			Destroy(gameObject);
		}

		public void ChangeStatesFun(EnemyStates enemyState,GameObject opponent)
		{
			EnemyState = enemyState;
			print("State has changed to" + EnemyState);
			
			if (EnemyState == EnemyStates.Idle)
			{
				StartCoroutine(IdleStateCoroutine());
			}

			if (EnemyState == EnemyStates.Attack)
			{
				StartCoroutine(AttackStateCoroutine(opponent));
			}

			if (EnemyState == EnemyStates.Searching)
			{
				StartCoroutine(SearchingStateCoroutine());
			}
		}

		public IEnumerator SearchingStateCoroutine()
		{
			print("Searching Coroutine");
			
			Vector3 target;
			var timer = 5;
			while (EnemyState == EnemyStates.Searching)
			{
				yield return new WaitForSeconds(1);
				target = new Vector3(Random.Range(1, Speed), transform.position.y, Random.Range(1, Speed));
				Agent.SetDestination(target);
				timer -= 1;
				if (timer <= 0)
				{
					print("Changed to Idle");
					ChangeStatesFun(EnemyStates.Idle,null);
				}
			}
		}
		
		public IEnumerator IdleStateCoroutine()
		{
			Vector3 target;
				while (EnemyState == EnemyStates.Idle)
				{
					yield return new WaitForSeconds(3);
					target = new Vector3(Random.Range(1, Speed), transform.position.y, Random.Range(1, Speed));
					Agent.SetDestination(target);
				}
		}

		public IEnumerator AttackStateCoroutine(GameObject opponent)
		{
			print("Attak Coroutine");
			
			while (EnemyState == EnemyStates.Attack)
			{
				Agent.SetDestination(opponent.transform.position);
				
				if (Vector3.Distance(opponent.transform.position,transform.position) < 0.5f)
				{
					if (_canAttack)
					{
						print("Attack?");
						StartCoroutine(Attack(opponent));
					}
				}

				if (Vector3.Distance(opponent.transform.position,transform.position) > GetComponent<BoxCollider>().size.z)
				{
					ChangeStatesFun(EnemyStates.Searching,null);
				}
				
				yield return null;
			}
		}

		public void SetLootOnEnemy(List<Item> items)
		{
			for (int i = 0; i < HowManyLootOnEnemy; i++)
			{
				LootOnEnemy.Add(items[Random.Range(0,items.Count)]);
			}
			
			LootOnEnemy.Add(new Item(null,1,"",Random.Range(3,5),false, 0, ItemsTypes.Xp,10000001,true));
		}
		
		public void DropLoot()
		{
			var lootPocket = Instantiate(LootPocket);
			lootPocket.transform.position = gameObject.transform.position;
			RaycastHit hit;
			Physics.Raycast(lootPocket.transform.position, -lootPocket.transform.up, out hit);
			lootPocket.transform.position = hit.transform.position;
			
			var lootInLootPocket = lootPocket.GetComponent<ExpandableItemProvider>();
			
			foreach (var loot in LootOnEnemy)
			{
				lootInLootPocket.ItemsInProvider.Add(new Item(loot.ItemGameObject,loot.AmountOfItem,loot.InfoAbout,loot.AmountOfResource,loot.IsUseble,loot.MoneyCost,loot.ItemType,loot.IDofObject,loot.IsItNoStaking));
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<Char>() != null)
			{
				print("Trigger enter Attack");
				ChangeStatesFun(EnemyStates.Attack,other.gameObject);
			}
		}

		private IEnumerator Attack(GameObject other)
		{
			if (other.GetComponent<Char>() != null)
			{
				_canAttack = false;
				other.GetComponent<Char>().HpNow -= AttackPower;
			}
			
			yield return new WaitForSeconds(ReloadSpeed);
			_canAttack = true;
		}

		public void StartCoroutineDamage(int amount)
		{
		    //StartCoroutine(DamageCoroutine(amount));
			Hp -= amount;
		}
	}
}
