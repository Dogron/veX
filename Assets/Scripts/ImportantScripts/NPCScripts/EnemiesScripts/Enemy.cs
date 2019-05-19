using System;
using System.Collections;
using System.Collections.Generic;
using ImportantScripts.ItemsScripts;

using ResourcesAndItems;
using UnityEngine;
using UnityEngine.AI;
using Char = ImportantScripts.CharScripts.Char;
using Random = UnityEngine.Random;

namespace ImportantScripts.NPCScripts.EnemiesScripts
{
	public class Enemy : MonoBehaviour
	{
		public NavMeshAgent agent;

		public int hp;

		public bool _canAttack = true;

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

		public delegate void DieDelegate(int a);
		public DieDelegate d1;
		public DieDelegate d2;
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

			d1 = Char.CharIn.GetXp;
			d2 = Char.CharIn.GetMoney;
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

		protected void ChangeStatesFun(EnemyStates enemyState,GameObject opponent)
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
					AttackStateCoroutine(opponent);
				    break;
				case EnemyStates.Searching:
					StartCoroutine(SearchingStateCoroutine(opponent));
				    break;
				default:
					throw new ArgumentOutOfRangeException(nameof(enemyState), enemyState, null);
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

				if (Vector3.Distance(opponent.transform.position, gameObject.transform.position) <= radiusOfSee)
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

		private void AttackStateCoroutine(GameObject opponent)
		{
			StartCoroutine(Attack(opponent));
		}

		public void SetLootOnEnemy(List<Item> items)
		{
			for (var i = 0; i < howManyLootOnEnemy; i++)
			{
				lootOnEnemy.Add(items[Random.Range(0,items.Count)]);
			}
		}

		private void DropLoot()
		{
			var o = gameObject;
			// ReSharper disable once InconsistentNaming
			
			var _lootPocket = Instantiate(lootPocket,o.transform.position,o.transform.rotation);
			
			var lootInLootPocket = _lootPocket.GetComponent<ExpandableItemProvider>();
			
			foreach (var loot in lootOnEnemy)
			{
				lootInLootPocket.ItemsInProvider.Add(new Item(loot.itemGameObject,loot.amountOfItem,loot.infoAbout,loot.amountOfResource,loot.isUseble,loot.moneyCost,loot.ItemType,loot.IsItNoStaking,loot.Name));
			}

			d2(MoneyOnEnemy);
			d1(XpOnEnemy);
			
		}

		protected virtual IEnumerator Attack(GameObject other)
		{
			yield return null;
		}

		public void StartCoroutineDamage(int amount)
		{
			Damage(amount);
		}
	}
}
