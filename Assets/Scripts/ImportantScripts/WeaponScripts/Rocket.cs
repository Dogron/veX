using System.Collections;
using ImportantScripts.CharScripts;
using ImportantScripts.Interactables;
using ImportantScripts.NPCScripts;
using ImportantScripts.NPCScripts.EnemiesScripts;
using UnityEngine;

namespace ImportantScripts.WeaponScripts
{
	public class Rocket : MonoBehaviour {

		public int Speed = 300;
		public int Damage = 2;
		public float Duration = 1;

		public SphereCollider SphereCollider;
		public GameObject TailOfBoom;
		
		private float _timePassed;

		private void Update()
		{
			var deltaTime = Time.deltaTime;

			_timePassed += deltaTime;
			if (_timePassed > Duration)
			{
				Destroy(gameObject);
				return;
			}

			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, Speed * deltaTime))
			{
				var someThing = hit.collider.gameObject;
				print(someThing.gameObject.name);
				
				if (someThing != null)
			    {
				    if (someThing.GetComponent<Char>() == null)
				    {
					    Instantiate(TailOfBoom, transform.position, transform.rotation);
					    SphereCollider.enabled = true;
				    }
			    }
			}

			transform.position += deltaTime * Speed * transform.forward;
		}

		private void OnTriggerEnter(Collider other)
        {
			         print("BlaBlaBLa");

	        var enemy = other.gameObject.GetComponent<Enemy>();
	        
	        if (enemy == null)
	        {
		        var damagable = other.gameObject.GetComponent<Damagable>();
		        if (damagable != null)
		        {
			        damagable.TakeDamage(Damage);
		        }
                   
	        }

	        else if (enemy != null)
	        {
		        enemy.StartCoroutineDamage(1);
		        Instantiate(TailOfBoom, other.gameObject.transform.position, other.gameObject.transform.rotation);
	        }

		}
	}
	
	  
}
