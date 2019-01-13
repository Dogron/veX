using UnityEngine;
   
   namespace ImportantScripts.WeaponScripts
   {
       public interface IWeapon
       {
           int AmmoNow { get; }
           int AmmoMax { get; }
           string Name { get; }
           void Fire(Vector3 position, Quaternion rotation);
           int Reload(int available);
       }
   }