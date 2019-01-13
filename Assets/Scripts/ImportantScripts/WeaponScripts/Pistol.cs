using UnityEngine;

namespace ImportantScripts.WeaponScripts
{
    public sealed class Pistol : MonoBehaviour, IWeapon
    {
        public GameObject Bullet;

        private int _ammoNow = 100;

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

        public void Fire(Vector3 position, Quaternion rotation)
        {
            if (_ammoNow == 0)
                return;

            _ammoNow--;
            Instantiate(Bullet, position, rotation);
        }

        public int Reload(int available)
        {
            var missing = AmmoMax - AmmoNow;
            if (available <= missing)
            {
                _ammoNow += available;
                return available;
            }
            else
            {
                _ammoNow += missing;
                return missing;
            }
        }
    }
}