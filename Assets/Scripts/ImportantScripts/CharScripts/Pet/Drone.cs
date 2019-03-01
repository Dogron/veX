using System.Collections;
using System.Collections.Generic;
using ImportantScripts.ItemsScripts;
using ResourcesAndItems;
using UnityEngine;

namespace ImportantScripts.CharScripts.Pet
{
    public class Drone : MonoBehaviour
    {
        public int Speed;
        
        public GameObject Char;

        public bool IsMoving;
        
        public Inventory Inventory;
        public SphereCollider SphereCollider;

        public Vector3 Distanation;
        
        private void Start()
        {
            IsMoving = true;
            Char = CharScripts.Char.CharIn.gameObject;
            Inventory = Char.GetComponent<Inventory>();
            
            gameObject.transform.position = Char.transform.position;
           
        }

        private void Update()
        {
            StartCoroutine(Move(Distanation));
        }

        private IEnumerator Move(Vector3 distanation)
        {
            if (IsMoving) 
            {
                print("Is moving!!11!");

                if (Vector3.Distance(gameObject.transform.position, distanation) < 1)
                {
                    transform.rotation = new Quaternion(0,0,0,0);
                    SphereCollider.enabled = true;
                    yield return new WaitForSeconds(3);
                    SphereCollider.enabled = false;
                    IsMoving = false;
                }

                else
                {
                    gameObject.transform.LookAt(distanation);
                    transform.position += Time.deltaTime * Speed * transform.forward;
                }
            }

            else
            {
                gameObject.transform.LookAt(Char.transform);
                transform.position += Time.deltaTime * Speed * transform.forward;

                if (Vector3.Distance(gameObject.transform.position,Char.transform.position) < 0.3f)
                {
                    Destroy(gameObject);
                } 
            }
               
            
        }

        private void OnTriggerStay(Collider other)
        {
            var provider = other.GetComponent<ExpandableItemProvider>();
            var addedItems = new List<Item>();
            
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
                            addedItems.Add(item);
                        }

                        else
                        {
                            break;
                        }
                    }

                    foreach (var t in addedItems)
                    {
                        if (provider.gameObject.GetComponent<ExpandableItemProvider>())
                        {
                            provider.ItemsInProvider.Remove(t);
                        }  
                    }
				
                    addedItems.Clear();
                }
            }
        }
    }
}