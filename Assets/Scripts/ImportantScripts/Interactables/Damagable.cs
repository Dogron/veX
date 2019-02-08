using UnityEngine;

namespace ImportantScripts.Interactables
{
    public class Damagable : MonoBehaviour
    {
        public int Hp = 5;

        public void TakeDamage(int howManyDamage)
        {
            Hp -= howManyDamage;
        }

        private void Update()
        {
            if (Hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}