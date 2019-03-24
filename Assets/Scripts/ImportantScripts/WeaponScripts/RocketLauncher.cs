using ImportantScripts.Managers;
using UnityEngine;

namespace ImportantScripts.WeaponScripts
{
    public sealed class RocketLauncher : MonoBehaviour, IWeapon
    {
        public GameObject Rocket;

        private int _ammoNow = 10;

        public float LastShot { get; private set; }
        public float RateOfFire;

        public int AmmoNow
        {
            get { return _ammoNow; }
        }

        public int AmmoMax
        {
            get { return 10; }
        }

        public string Name
        {
            get { return "RocketLauncher"; }
        }
        
        public void Fire(Vector3 position, Quaternion rotation, int additionalDamage)
        {
            print("SomeThing");
            if (_ammoNow == 0)
                return;

            if (!(Time.time - LastShot > RateOfFire)) 
                return;

            LastShot = Time.time;
            _ammoNow--;
           
            var bull = ObjectPoolManager.Instance.ChooseListToPool(Rocket,position,rotation);
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
            
            {
                _ammoNow += missing;
                return missing;
            }
        }
    }
}