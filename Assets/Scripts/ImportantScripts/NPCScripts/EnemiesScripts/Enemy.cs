using System.Collections;
using System.Collections.Generic;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using ImportantScripts.Managers;
using ResourcesAndItems;
using UnityEngine;
using UnityEngine.AI;

namespace ImportantScripts.NPCScripts.EnemiesScripts
{
	public class Enemy : MonoBehaviour
	{
		public NavMeshAgent agent;

		public int hp;

		private bool _canAttack = true;

		// ReSharper disable once InconsistentNaming
		public EnemyStates EnemyState;
		
		public int speed;
		
		private Coroutine _coroutine;
		public int attackPower;
		public int reloadSpeed;
		public int radiusOfSee;
		
		public List<Item> lootOnEnemy;
		public int MoneyOnEnemy;
		public int XpOnEnemy;

		
		public GameObject lootPocket;
		
		public int howManyLootOnEnemy;

		private void Start()
		{
		    ChangeStatesFun(EnemyStates.Idle,null);
			MoneyOnEnemy = MoneyOnEnemy + Random.Range(-30, 30);
			if (MoneyOnEnemy < 0)
			{
				MoneyOnEnemy = 0;
			}
			
			XpOnEnemy = XpOnEnemy + Random.Range(0, 3);
		}

		public void ChooseHowManyLootOnEnemy()
		{
			howManyLootOnEnemy = Random.Range(1, 4);
		}

		private void Damage(int damage)
		{
			hp -= damage;

			if (hp > 0) return;
			
			DropLoot();
			Destroy(gameObject);
		}

		private void ChangeStatesFun(EnemyStates enemyState,GameObject opponent)
		{
			opponent = Char.CharIn.gameObject;
			EnemyState = enemyState;
			//print("State has changed to" + EnemyState);
			
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
			}

			switch (enemyState)
			{
				case EnemyStates.Idle:
					StartCoroutine(IdleStateCoroutine(opponent));
					break;
				case EnemyStates.Attack:
					print("Attack");
					StartCoroutine(AttackStateCoroutine(opponent));
				    break;
				case EnemyStates.Searching:
					StartCoroutine(SearchingStateCoroutine(opponent));
				    break;
			}
		}

		private IEnumerator SearchingStateCoroutine(GameObject opponent)
		{
			print("Searching Coroutine");

			var timer = 5;
			while (EnemyState == EnemyStates.Searching)
			{
				yield return new WaitForSeconds(1);
				var position = transform.position;
				var target = position + new Vector3(Random.Range(1, speed), position.y, Random.Range(1, speed));
				agent.SetDestination(target);
				timer -= 1;
				if (timer <= 0)
				{
					print("Changed to Idle");
					ChangeStatesFun(EnemyStates.Idle, null);
				}

				if (Vector3.Distance(opponent.transform.position, gameObject.transform.position) < radiusOfSee)
				{
					ChangeStatesFun(EnemyStates.Attack,opponent);
				}
			}
		}

		private IEnumerator IdleStateCoroutine(GameObject opponent)
		{
			while (EnemyState == EnemyStates.Idle)
				{
					yield return new WaitForSeconds(3);

					var position = transform.position;
					var target = position + new Vector3(Random.Range(1, speed), position.y, Random.Range(1, speed));
					agent.SetDestination(target);

					if (Vector3.Distance(opponent.transform.position, gameObject.transform.position) < radiusOfSee)
					{
						ChangeStatesFun(EnemyStates.Attack,opponent);
					}
				}
		}

		private IEnumerator AttackStateCoroutine(GameObject opponent)
		{
			print("Attack Coroutine");
			
			while (EnemyState == EnemyStates.Attack)
			{
				agent.SetDestination(opponent.transform.position);
				
				if (Vector3.Distance(opponent.transform.position,gameObject.transform.position) < 3f)
				{
					print("distance");
					
					if (_canAttack)
					{
						print("Attack?");
						StartCoroutine(Attack(opponent));
					}
				}

				if (Vector3.Distance(opponent.transform.position,transform.position) > radiusOfSee)
				{
					ChangeStatesFun(EnemyStates.Searching,null);
				}
				
				yield return null;
			}
		}

		public void SetLootOnEnemy(List<Item> items)
		{
			for (int i = 0; i < howManyLootOnEnemy; i++)
			{
				lootOnEnemy.Add(items[Random.Range(0,items.Count)]);
			}
		}
		
		public void DropLoot()
		{
			// ReSharper disable once InconsistentNaming
			var _lootPocket = Instantiate(lootPocket,gameObject.transform.position,gameObject.transform.rotation);
			
			var lootInLootPocket = _lootPocket.GetComponent<ExpandableItemProvider>();
			
			foreach (var loot in lootOnEnemy)
			{
				lootInLootPocket.ItemsInProvider.Add(new Item(loot.itemGameObject,loot.amountOfItem,loot.infoAbout,loot.amountOfResource,loot.isUseble,loot.moneyCost,loot.ItemType,loot.IsItNoStaking,loot.Name));
			}

			Char.CharIn.GetXp(XpOnEnemy);
			Char.CharIn.Money += MoneyOnEnemy;

		}

		private IEnumerator Attack(GameObject other)
		{
			var charComp = other.GetComponent<Char>();
			print(charComp != null);
			
			if (charComp != null)
			{
				_canAttack = false;
				print(other);
				print("Attack");
				charComp.HpNow = charComp.HpNow - attackPower;
			}

			yield return new WaitForSeconds(reloadSpeed);
			_canAttack = true;
		}

		public void StartCoroutineDamage(int amount)
		{
			Damage(amount);
		}
	}
}
