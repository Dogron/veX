using System.Threading.Tasks;
using ImportantScripts.CharScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UselessScripts
{
    public class NameTextMeshPro : MonoBehaviour
    {
        public GameObject target;

        private async void Start()
        {
            await Task.Delay(150);
           
            var lc = target.transform.localScale;
            var midlleNub = (lc.x + lc.y + lc.z) / 3;
            var transform1 = gameObject.transform;
            transform1.localScale = new Vector3(midlleNub/2, midlleNub/2, midlleNub/2);
            var position = target.transform.position;
            transform1.position = new Vector3(position.x,position.y + midlleNub * 1.5f,position.z);
            
            GetComponentInChildren<TextMeshPro>().text = target.name;
        }


        private void Update()
        {
            transform.LookAt(Char.CharIn.gameObject.transform);
        }
    }
}