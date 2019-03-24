using System.Collections;
using Cinemachine;
using UnityEngine;

namespace UselessScripts
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineVirtualCamera mainVcCamera;
        public CinemachineVirtualCamera[] otherVirtualCameras;
        
       /* private IEnumerator SwitchCamera()
        {
            foreach (var camera in _otherVirtualCameras)
            {
                camera.Priority = 2;
                yield return new WaitForSeconds(1);
                camera.Priority = 0;
            }
        }
     */

       private void SwitchCamera()
       {
           print("CameraSwitch");
           
           for (int i = 0; i < otherVirtualCameras.Length; i++)
           {
                   if (otherVirtualCameras[i].Priority == 2)
                   {
                       if (otherVirtualCameras.Length > i)
                       {
                           otherVirtualCameras[i].Priority = 0;
                           otherVirtualCameras[i + 1].Priority = 2;
                           break;
                       }

                       otherVirtualCameras[i].Priority = 0;
                       break;
                   }

                   otherVirtualCameras[0].Priority = 2;
           }
       }
           
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SwitchCamera();
            }
        }
    }
}