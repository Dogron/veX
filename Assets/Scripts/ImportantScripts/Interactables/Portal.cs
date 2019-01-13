using ImportantScripts.Managers;
using UnityEngine;

namespace UselessScripts
{
    public class Portal : MonoBehaviour {
    
        public GameObject ExitZone;
    
        public Vector3 ShiftPos;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Tp();
            }
        }

        public void Tp()
        {
            GameManager.GameManagerIn.Char.transform.position = ExitZone.transform.position + ShiftPos;
        }
    }
}

