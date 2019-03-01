
using System.Collections;
using System.Collections.Generic;
using ImportantScripts.NPCScripts.EnemiesScripts;
using UnityEngine;

namespace ImportantScripts.NPCScripts
{
    public class BulletTurret : MonoBehaviour
    {
        public List<Enemy> Enemies;
        public GameObject _currentTarget;
        public GameObject TurretTower;
        public GameObject Bullet;

        public bool IsItFixed;
        
        public int ReloadSpeed;

        private void Start()
        {
            StartCoroutine(ShootIEnumerator());
        }

        public void Fix()
        {
            IsItFixed = true;
        }
        public void ShootCoroutine()
        {
            if (!IsItFixed) return;
            
            var minDistance = 0f;

            if (Enemies != null)
            {
                foreach (var enemy in Enemies)
                {
                    if (enemy == null) continue;
                    if (!(Vector3.Distance(gameObject.transform.position, enemy.gameObject.transform.position) >
                          minDistance)) continue;

                    minDistance = Vector3.Distance(gameObject.transform.position,
                        enemy.gameObject.transform.position);
                    _currentTarget = enemy.gameObject;
                }

                if (_currentTarget != null)
                {
                    TurretTower.transform.LookAt(_currentTarget.transform);
                    Instantiate(Bullet, TurretTower.transform.position + TurretTower.transform.forward, TurretTower.transform.rotation);
                }

                Enemies.Clear();
            }

            else
            {
                _currentTarget = null;
            }

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                if (Enemies.Contains(other.gameObject.GetComponent<Enemy>()) == false)
                {
                    Enemies.Add(other.gameObject.GetComponent<Enemy>());
                    print("enemy added"); 
                } 
            }
        }

        private IEnumerator ShootIEnumerator()
        {
            while (true)
            {
                    yield return new WaitForSeconds(ReloadSpeed);
                    ShootCoroutine();
                    yield return null;
            } 
            // ReSharper disable once IteratorNeverReturns
        }
    }
}