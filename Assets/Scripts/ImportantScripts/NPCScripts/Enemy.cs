using System.Collections;
using ImportantScripts.Managers;
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

		public Material[] AllMatInChildren;
		public Color[] AllColorsInChildren;
		
		private void Start()
		{
			
		}


		private void Update ()
		
		{
			Agent.SetDestination(GameManager.GameManagerIn.Char.transform.position);

			if (Hp <= 0)
			{
				Destroy(gameObject);
			}

			if (Hp <= 0)
			{
				Destroy(gameObject);
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
