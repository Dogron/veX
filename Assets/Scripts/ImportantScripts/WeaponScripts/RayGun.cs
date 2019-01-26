using UnityEngine;

namespace ImportantScripts.WeaponScripts
{
    public class RayGun : MonoBehaviour, IWeapon
    {
        private int _ammoNow = 0;
        
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
            get { return "RayGun"; }
        }

        public void Fire(Vector3 position, Quaternion rotation)
        {
            throw new System.NotImplementedException();
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