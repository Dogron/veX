using System.Collections;
using System.Collections.Generic;
using ImportantScripts.ItemsScripts;
using ImportantScripts.Managers;
using ResourcesAndItems;
using UnityEngine;
using UnityEngine.AI;

namespace ImportantScripts
{
	public class Enemy : MonoBehaviour
	{

		public NavMeshAgent Agent;

		public int Hp;

		private bool _canAttack = true;

		public int AttackPower;
		public int ReloadSpeed;

		public List<Item> LootOnEnemy;

		public GameObject LootPocket;
		
		public Material[] AllMatInChildren;
		public Color[] AllColorsInChildren;

		public int HowManyLootOnEnemy;

		public void ChooseHowManyLootOnEnemy()
		{
			HowManyLootOnEnemy = Random.Range(1, 4);
		}


		private void Update ()
		{
			Agent.SetDestination(GameManager.GameManagerIn.Char.transform.position);

			if (Hp <= 0)
			{
				DropLoot();
				Destroy(gameObject);
			}
		}

		public void SetLootOnEnemy(List<Item> items)
		{
			for (int i = 0; i < HowManyLootOnEnemy; i++)
			{
				LootOnEnemy.Add(items[Random.Range(0,items.Count)]);
			}
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
				lootInLootPocket.ItemsInProvider.Add(loot);
			}
		}
		
		private void OnTriggerEnter(Collider other)
		{
			if (!_canAttack) return;
			if (other.gameObject.GetComponent<Char>() != null)
			{
				StartCoroutine(Attack(other));
			}
		}

		private IEnumerator Attack(Component other)
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

		IEnumerator DamageCoroutine(int amount)
		{
			foreach (var matInChildren in AllMatInChildren)
			{
				matInChildren.color = Color.red;
			}
			
			yield return new WaitForSeconds(0.3f);

			for (int i = 0; i < AllMatInChildren.Length; i++)
			{
				AllMatInChildren[i].color = AllColorsInChildren[i];
			}
			
		}
	}
}
