using System;
using System.Collections.Generic;
using ImportantScripts.Interactables;
using ImportantScripts.Managers;
using ImportantScripts.NPCScripts;
using ImportantScripts.WeaponScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace ImportantScripts
{
    public class Char : MonoBehaviour
    {
       
        public GameObject HeadNeckChar;

        public IWeapon CurrentWeapon;

        public static Char CharIn;

        public Rigidbody MyRigid;

        public float Speed;
        private float _speed;

        public List<GameObject> Inventory;
        
        public int JumpPower;
        public int MaxHp;
        public int HpNow;
        public int TotalAmmoBullets;
        public int TotalAmmoRockets;
        public int Money;

        public bool _inDialogue;
        
        public bool RocketLauncherPickedUp;
        
        public float LookSpeed = 5f;
        private Vector3 _movement;

        private Dialogue _npc;
       
        
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
           
            
            if (Input.GetKey(KeyCode.A))
               MyRigid.AddForce(_speed * -transform.right, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.D))
                MyRigid.AddForce(_speed * transform.right, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.W))
                MyRigid.AddForce(_speed * transform.forward, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.S))
                MyRigid.AddForce(_speed * -transform.forward, ForceMode.Acceleration);

            var cameramain = GameManager.GameManagerIn.Camera;

            
            if (_inDialogue == false)
            {
            
            gameObject.transform.rotation =
                new Quaternion(0.0f, cameramain.transform.rotation.y, 0, cameramain.transform.rotation.w);

            HeadNeckChar.transform.rotation = cameramain.transform.rotation;

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

                if (Input.GetKeyDown(KeyCode.Mouse0) && CurrentWeapon != null)
                {
                    var weaponTransform = cameramain.transform;
                    CurrentWeapon.Fire(weaponTransform.position, weaponTransform.rotation);
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
                        var provider = hitObject.GetComponent<ResourceProvider>();
                         _npc = hitObject.GetComponent<Dialogue>();

                        if (provider != null)
                        {
                            var resource = provider.Consume();

                            if (resource != null)
                            {
                                switch (resource.Type)
                                {
                                    case ResourceType.Health:
                                        Heal(resource.Amount);
                                        break;
                                    case ResourceType.Bullet:
                                        TotalAmmoBullets += resource.Amount;
                                        break;
                                    case ResourceType.Rocket:
                                        TotalAmmoRockets += resource.Amount;
                                        break;
                                    case ResourceType.RocketLauncher:
                                        RocketLauncherPickedUp = true;
                                        break;
                                    case ResourceType.QuestItem:
                                        for (int i = 0; i < resource.Amount; i++)
                                        {
                                            Inventory.Add(hitObject);
                                        }
                                        break;
                                    case ResourceType.Bush:
                                        var j = hitObject.GetComponent<BushWithBerries>().TypeOfBerries;
                                        
                                        switch (j)
                                        {
                                            case TypesOfBerries.Heal:
                                                Heal(resource.Amount);
                                                break;
                                            case TypesOfBerries.Poison:
                                                Heal(-resource.Amount);
                                                break;
                                            default:
                                                throw new ArgumentOutOfRangeException();
                                        }

                                        break;
                                    case ResourceType.Money:
                                        Money += resource.Amount + Random.Range(-resource.Amount / 100 * 20,
                                        resource.Amount / 100 * 20);
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }
                        }

                        if (_npc != null)
                        {
                            _inDialogue = true;
                            _npc.ShowDialogue = true;
                            _npc.WhoIsTalking = gameObject;
                            _npc.UpdateNodeAndAnswers();
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

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Npc>() != null)
            {
                _inDialogue = false;
            }
        }
    }
}