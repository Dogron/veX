using UnityEngine;

namespace ImportantScripts
{
    public class Interactable : MonoBehaviour
    {
        public bool PlayerNearby;
   
   
        public virtual void StartInteract()
        {
       
        }
      
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            PlayerNearby = true;
        }

        public void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            PlayerNearby = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && PlayerNearby)
            {
                StartInteract();
            }
        }

    }
}
