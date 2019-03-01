using ImportantScripts.Interactables;
using ImportantScripts.NPCScripts;
using ImportantScripts.NPCScripts.EnemiesScripts;
using UnityEngine;

namespace ImportantScripts.WeaponScripts
{
    public class Bullet : MonoBehaviour
    {
        public int Speed = 300;
        public int Damage = 1;
        public float Duration = 1;

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
                var enemy = hit.collider.gameObject.GetComponent<Enemy>();

                if (enemy == null)
                {
                    var damagable = hit.collider.gameObject.GetComponent<Damagable>();
                    if (damagable != null)
                    {
                        damagable.TakeDamage(Damage);
                    }
                   
                }

                else if (enemy != null)
                {
                    enemy.StartCoroutineDamage(Damage);
                    Instantiate(TailOfBoom, hit.transform.position, hit.transform.rotation);
                }

                Destroy(gameObject);
                return;
            }

            transform.position += deltaTime * Speed * transform.forward;
        }
    }
}