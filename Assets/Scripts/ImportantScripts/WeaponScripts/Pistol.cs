using ImportantScripts.Managers;
using UnityEngine;

namespace ImportantScripts.WeaponScripts
{
    public sealed class Pistol : MonoBehaviour, IWeapon
    {
        public GameObject bullet;

        private int _ammoNow = 100;

        public float LastShot { get; private set; }
        public float rateOfFire;

        public int AmmoNow
        {
            get { return _ammoNow; }
        }

        public int AmmoMax
        {
            get { return 100; }
        }

        public string Name
        {
            get { return "Pistol"; }
        }

        public void Fire(Vector3 position, Quaternion rotation,int additionalDamage)
        {
            if (_ammoNow == 0)
                return;

            if (!(Time.time - LastShot > rateOfFire)) 
                return;

            LastShot = Time.time;
            
            _ammoNow--;
           
           var bull = ObjectPoolManager.Instance.ChooseListToPool(bullet,position,rotation);
          print(bull != null);
           bull.GetComponent<Bullet>().Damage += additionalDamage;
        }

        public int Reload(int available)
        {
            var missing = AmmoMax - AmmoNow;
            if (available <= missing)
            {
                _ammoNow += available;
                return available;
            }

            _ammoNow += missing;
            return missing;
        }
    }
}