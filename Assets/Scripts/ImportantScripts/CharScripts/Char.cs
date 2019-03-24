using System;
using System.Collections.Generic;
using ImportantScripts.CharScripts.Pet;
using ImportantScripts.Interactables;
using ImportantScripts.ItemsScripts;
using ImportantScripts.Managers;
using ImportantScripts.NPCScripts;
using ImportantScripts.WeaponScripts;
using ResourcesAndItems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UselessScripts;

namespace ImportantScripts.CharScripts
{
    public class Char : MonoBehaviour
    {
        public List<Item> AddedItems;
        
        public GameObject HeadNeckChar;

        public IWeapon CurrentWeapon;

        public static Char CharIn;

        public Rigidbody MyRigid;

        public float Speed;
        private float _speed;

        public Inventory Inventory;
        
        //Skills
        public int AdditionalDamage;
        //Skills

        public GameObject DroneNow;
        public GameObject Drone;
        
        
        public int JumpPower;
        public int MaxHp;
        public int HpNow;
        public int TotalAmmoBullets;
        public int TotalAmmoRockets;
        public int Money;

        public bool IsDronePickedUp;
        
        public int Xp;
        public int LvlOfChar;
        public int XpReqForNewLvl;
        public int CurrAmountOfSkillPoints;
        
        public bool InDialogue;
        public bool IsInstrumentsPickedUp;
        
        public bool RocketLauncherPickedUp;
        
        public float LookSpeed = 5f;
        private Vector3 _movement;

        public void StatsUp(string whatStat)
        {
            if (CurrAmountOfSkillPoints > 0)
            {
                switch (whatStat)
                {
                    case "Hp":
                        MaxHp += 3;
                        Heal(999);
                        break;
                    case "Damage":
                        AdditionalDamage += 3;
                        break;
                    case "Speed":
                        Speed += 2;
                        break;
                }

                CurrAmountOfSkillPoints -= 1;
            }
        }
        
        public void LvlUp()
        {
            LvlOfChar += 1;

            if (LvlOfChar % 3 == 0)
            {
                CurrAmountOfSkillPoints += 2;
            }

            else
            {
                CurrAmountOfSkillPoints += 1;
            }
        }
        
        
        public void GetXp(int amountOfXp)
        {
            Xp += amountOfXp;

            while (Xp >= XpReqForNewLvl)
            {
                Xp -= XpReqForNewLvl;
                XpReqForNewLvl += XpReqForNewLvl * 50 / 100;
                LvlUp();
            }
        }
        
        private void Awake()
        {
            CharIn = this;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            HpNow = MaxHp;
            _speed = Speed;
        }
        
        private void Update()
        {
            var cameramain = GameManager.GameManagerIn.Camera;
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (IsDronePickedUp)
                {
                    if (DroneNow == null)
                    {
                        var drone = Instantiate(Drone);
                        DroneNow = drone;
                        
                        var lookingTransform = cameramain.transform;
                        RaycastHit hit;
                        if (Physics.Raycast(lookingTransform.position + lookingTransform.forward * 0.1f,
                            lookingTransform.forward, out hit, 9999))
                        {
                            drone.GetComponent<Drone>().Distanation = hit.point;    
                        }  
                    }  
                }
            }
            
            if (InDialogue == false)
            {
            gameObject.transform.rotation =
                new Quaternion(0.0f, cameramain.transform.rotation.y, 0, cameramain.transform.rotation.w);

            HeadNeckChar.transform.rotation = cameramain.transform.rotation;

                if (Input.GetKey(KeyCode.A))
                    MyRigid.AddForce(_speed * -transform.right, ForceMode.Acceleration);
                if (Input.GetKey(KeyCode.D))
                    MyRigid.AddForce(_speed * transform.right, ForceMode.Acceleration);
                if (Input.GetKey(KeyCode.W))
                    MyRigid.AddForce(_speed * transform.forward, ForceMode.Acceleration);    
                if (Input.GetKey(KeyCode.S))
                    MyRigid.AddForce(_speed * -transform.forward, ForceMode.Acceleration);
                
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _speed = Speed * 1.5f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _speed = Speed;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    print("Weapon now is Pistol");
                    CurrentWeapon = gameObject.GetComponentInChildren<Pistol>();
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (RocketLauncherPickedUp)
                    {
                        print("Weapon now is RocketLauncher");
                        CurrentWeapon = gameObject.GetComponentInChildren<RocketLauncher>(); 
                    } 
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    print("Wepaon now is Autorifle");
                    CurrentWeapon = gameObject.GetComponentInChildren<Autorifle>();
                }
                
                if (Input.GetKey(KeyCode.Mouse0) && CurrentWeapon != null)
                {
                    var weaponTransform = cameramain.transform;
                    CurrentWeapon.Fire(weaponTransform.position, weaponTransform.rotation, AdditionalDamage);
                }

                if (Input.GetKeyDown(KeyCode.R) && CurrentWeapon != null)
                {
                    if (CurrentWeapon.Name.Contains("Pistol"))
                    {
                        TotalAmmoBullets -= CurrentWeapon.Reload(TotalAmmoBullets);
                    }

                    if (CurrentWeapon.Name.Contains("RocketLauncher"))
                    {
                       TotalAmmoRockets -= CurrentWeapon.Reload(TotalAmmoRockets);
                    }
                }

                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    var lookingTransform = cameramain.transform;
                    RaycastHit hit;
                    if (Physics.Raycast(lookingTransform.position + lookingTransform.forward * 0.1f,
                        lookingTransform.forward, out hit, 1))
                    {
                        var hitObject = hit.collider.gameObject;
                        Debug.Log("Trying to use " + hitObject.name);
                        var provider = hitObject.GetComponent<ItemProvider>();
                        var portal = hitObject.GetComponent<Portal>();
                        var turret = hitObject.GetComponent<BulletTurret>();
                        var npc = hitObject.GetComponent<Dialogue>();

                        if (portal != null && IsInstrumentsPickedUp)
                        {
                           portal.Fix(); 
                        }

                        if (turret != null && IsInstrumentsPickedUp)
                        {
                            turret.Fix();
                        }
                        
                        if (provider != null)
                        {
                            var items = provider.Consume();

                            if (items != null)
                            {
                                    foreach (var item in items)
                                    {
                                        if (Inventory.SizeOfInventory > Inventory.ItemsInInventory.Count)
                                        {
                                            print("Added to inventory???");
                                            Inventory.AddToInventory(item);
                                            AddedItems.Add(item);
                                        }

                                        else
                                        {
                                            break;
                                        }
                                    }

                                    foreach (var t in AddedItems)
                                    {
                                        if (provider.gameObject.GetComponent<ExpandableItemProvider>())
                                        {
                                            provider.ItemsInProvider.Remove(t);
                                        }  
                                    }
				
                                    AddedItems.Clear();
                            }
                        }
                         var resourceProvider = hitObject.GetComponent<ResourceProvider>();
    
                        if (resourceProvider != null)
                        {
                            var resource = resourceProvider.Consume();

                            if (resource != null)
                            {
                                // ReSharper disable once SwitchStatementMissingSomeCases
                                switch (resource.Type)
                                {
                                    case ResourceType.Bush:
                                        var bushWithBerries = hitObject.GetComponent<BushWithBerries>();
                                        Heal(bushWithBerries.Collect());
                                        print("SomeThing");
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }
                        }

                        if (npc != null)
                        {
                            try
                            {
                                npc.OnDialogueStart(gameObject);
                                InDialogue = true;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    }
                }
            }
            
            var yRot = Input.GetAxisRaw("Mouse X");
            var rotation = new Vector3(0f, yRot, 0f) * LookSpeed;
            MyRigid.MoveRotation(MyRigid.rotation * Quaternion.Euler(rotation));

            if (HpNow <= 0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

       public void OnUseItem(Item item)
       {
           var amountOfResource = item.OnUse();

           switch (item.ItemType)
           {
                   case ItemsTypes.Heal:
                       Heal(amountOfResource);
                       break;
                   case ItemsTypes.Quest:
                       break;
                   case ItemsTypes.RocketLauncher:
                       RocketLauncherPickedUp = true;
                       break;
                   case ItemsTypes.PocketWithMoney:
                       Money += amountOfResource;
                       break;
                   case ItemsTypes.Bullet:
                       TotalAmmoBullets += amountOfResource;
                       break;
                   case ItemsTypes.Rocket:
                       TotalAmmoRockets += amountOfResource;
                       break;
               case ItemsTypes.Key:
                   break;
               case ItemsTypes.Xp:
                   break;
               case ItemsTypes.Drone:
                   IsDronePickedUp = true;
                   break;
               default:
                       throw new ArgumentOutOfRangeException(); 
           }
       }
        
        private void OnCollisionStay()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MyRigid.AddForce(Vector3.up * JumpPower);
            }
        }

        public void Heal(int amountOfHealing)
        {
            var missing = MaxHp - HpNow;
           
            if (amountOfHealing <= missing)
            {
                HpNow += amountOfHealing;
                return;
            }

            if (amountOfHealing > missing)
            {
                HpNow += missing;
            }
        }
    }
}