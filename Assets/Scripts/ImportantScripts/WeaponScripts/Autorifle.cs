using ImportantScripts.Managers;
using UnityEngine;

namespace ImportantScripts.WeaponScripts
{
    public class Autorifle : MonoBehaviour, IWeapon
    {
        private int _ammoNow = 500;
        
        public GameObject Bullet;

        public float LastShot { get; private set; }
        public float RateOfFire;

        public int AmmoNow
        {
            get { return _ammoNow; }
        }

        public int AmmoMax
        {
            get { return 500; }
        }

        public string Name
        {
            get { return "Autorifle"; }
        }

        public void Fire(Vector3 position, Quaternion rotation, int additionalDamage)
        {
            if (_ammoNow == 0) 
                return;
            if (!(Time.time - LastShot > RateOfFire)) 
                return;

            LastShot = Time.time;
            
            _ammoNow--;
           
            var bull = ObjectPoolManager.Instance.ChooseListToPool(Bullet,position,rotation);
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