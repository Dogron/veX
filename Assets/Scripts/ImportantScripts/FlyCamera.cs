using UnityEngine;

namespace ImportantScripts
{
    public class FlyCamera : MonoBehaviour
    {

        public GameObject TargetToFollow;

        private float CamSens = 180;
        public float MinVert;
        public float MaxVert;
        private float _rotationX;

        private void Start()
        {
            transform.position = TargetToFollow.transform.position;
        }

        private void Update () {     
            

            var rotY = Input.GetAxis("Mouse Y");
            _rotationX -= rotY * CamSens * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, MinVert, MaxVert);
            var rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0f);
        }
     
    }
} 
 