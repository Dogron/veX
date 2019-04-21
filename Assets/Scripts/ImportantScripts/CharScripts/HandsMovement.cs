using UnityEngine;

namespace ImportantScripts.CharScripts
{
    public class HandsMovement : MonoBehaviour {
        void Update () {
            if (Camera.main != null)
            {
                var transform1 = Camera.main.transform;
                var transform2 = transform;
                transform2.position= transform1.position - Vector3.up*0.5f;
       
                Quaternion tempRot = transform1.rotation;
                transform.rotation = Quaternion.Lerp(transform2.rotation,tempRot,3*Time.deltaTime);
            }
        }
    }
}