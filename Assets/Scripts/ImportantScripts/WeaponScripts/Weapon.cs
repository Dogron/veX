using UnityEngine;
   
   namespace ImportantScripts.WeaponScripts
   {
       public interface IWeapon
       {
           float LastShot { get; }
           int AmmoNow { get; }
           int AmmoMax { get; }
           string Name { get; }
           void Fire(Vector3 position, Quaternion rotation,int addittionalDamage);
           int Reload(int available);
       }
   }