using System.Threading.Tasks;
using ResourcesAndItems;
using UnityEngine;
using UselessScripts;

namespace ImportantScripts
{
    public class FlyCamera : MonoBehaviour
    {

        public GameObject targetToFollow;

        public GameObject nameTextMeshPro;
        
        private float CamSens = 180;
        public float MinVert;
        public float MaxVert;
        private float _rotationX;

        private Transform _lookingtransform;
        private ExpandableItemProvider _expandableItemProviderNameToShow;

        public GameObject instansedNamePanel;
        private void Start()
        {
            transform.position = targetToFollow.transform.position;
        }

        private void Update () {     
            

            var rotY = Input.GetAxis("Mouse Y");
            _rotationX -= rotY * CamSens * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, MinVert, MaxVert);
            var transform1 = transform;
            var rotationY = transform1.localEulerAngles.y;
            transform1.localEulerAngles = new Vector3(_rotationX, rotationY, 0f);
            
            _lookingtransform = gameObject.transform;

            if (Physics.Raycast(_lookingtransform.position + _lookingtransform.forward * 0.1f,
                _lookingtransform.forward, out var hit, 1000))
            {
                if (hit.collider.gameObject.GetComponent<ExpandableItemProvider>() != null)
                {
                    if (instansedNamePanel == null && _expandableItemProviderNameToShow == null)
                    {
                        _expandableItemProviderNameToShow =
                            hit.collider.gameObject.GetComponent<ExpandableItemProvider>();
                        ShowNameOfObject();
                    }
                }
                
                else
                {
                    if (instansedNamePanel != null)
                    {
                       Destroy(instansedNamePanel);
                       instansedNamePanel = null;
                       _expandableItemProviderNameToShow = null;
                    }
                }
            }
        }

        private async void ShowNameOfObject()
        {
            //await Task.Delay(3000);
            
            if (Physics.Raycast(_lookingtransform.position + _lookingtransform.forward * 0.1f,
                _lookingtransform.forward, out var hit, 1000))
            {
                if (hit.collider.gameObject.GetComponent<ExpandableItemProvider>() == _expandableItemProviderNameToShow)
                {
                   instansedNamePanel = Instantiate(nameTextMeshPro);
                   instansedNamePanel.GetComponent<NameTextMeshPro>().target = hit.collider.gameObject;
                }
            }
        }
    }
} 
 