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

        void Update()
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
               
                if (enemy != null)
                {
                    enemy.StartCoroutineDamage(1);
                    Instantiate(TailOfBoom, transform.position, transform.rotation);
                }

                Destroy(gameObject);
                return;
            }

            transform.position += deltaTime * Speed * transform.forward;
        }
    }
}