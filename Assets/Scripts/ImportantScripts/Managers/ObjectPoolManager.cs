using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
// ReSharper disable InconsistentNaming

namespace ImportantScripts.Managers
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance;

        public GameObject Bullet;
        public List<GameObject> BulletList = new List<GameObject>();

        public GameObject Rocket;
        public List<GameObject> RocketsList = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
            SpawnObjects(Rocket, 40, RocketsList);
            SpawnObjects(Bullet, 40, BulletList);
        }

        public void SpawnObjects(GameObject objectToSpawn, int howManySpawn, List<GameObject> listToAssign)
        {
            for (var i = 0; i < howManySpawn; i++)
            {
                var temp = Instantiate(objectToSpawn, gameObject.transform);
                temp.SetActive(false);
                listToAssign.Add(temp);
            }
        }

        public void ChooseListToPool(GameObject gameObjectToPool, Vector3 _transform, Quaternion _quaternion)
        {
            if (gameObjectToPool.Equals(Bullet))
            {
               print("Bullet");
                
                PullObject(BulletList, _transform,_quaternion);
            }

            if (gameObjectToPool.Equals(Rocket))
            {
                print("Rocket");
                PullObject(RocketsList, _transform,_quaternion);
            }
        }

        private GameObject PullObject(IEnumerable<GameObject> listToPollFrom, Vector3 _transform, Quaternion _quaternion)
        {
            foreach (var gameObj in listToPollFrom)
            {
                if (!gameObj.activeInHierarchy)
                {
                    print("BulletSpawned");
                    gameObj.transform.rotation = _quaternion;
                    gameObj.transform.position = _transform;
                    gameObj.SetActive(true);
                    return gameObj;
                }
            }

            return null;
        }
    }
}