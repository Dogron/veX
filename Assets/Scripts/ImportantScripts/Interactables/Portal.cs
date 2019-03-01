using ImportantScripts.Managers;
using UnityEngine;

namespace UselessScripts
{
    public class Portal : MonoBehaviour {
    
        public GameObject ExitZone;
    
        public Vector3 ShiftPos;

        public bool IsWorking;

        public ParticleSystem ParticleSystem;

        private void Start()
        {
            ParticleSystem = GetComponentInChildren<ParticleSystem>();
            ParticleSystem.gameObject.SetActive(false);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Tp();
            }
        }

        public void Fix()
        {
            IsWorking = true;
            ParticleSystem.gameObject.SetActive(true);
        }
        
        public void Tp()
        {
            if (IsWorking)
            {
                GameManager.GameManagerIn.Char.transform.position = ExitZone.transform.position + ShiftPos; 
            }
        }
    }
}

