using UnityEngine;

namespace ImportantScripts.WeaponScripts
{
    public sealed class RocketLauncher : MonoBehaviour, IWeapon
    {
        public GameObject Rocket;

        private int _ammoNow = 10;

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
        
        
        public void Fire(Vector3 position, Quaternion rotation)
        {
            print("SomeThing");
            if (_ammoNow == 0)
                return;

            _ammoNow--;
            Instantiate(Rocket, position, rotation);
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