using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEnter : MonoBehaviour
{

    public Scene targetScene;
    
    void Start()
    {
        
    }

   
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           Tp(other.gameObject);
        }
    }
    
    public void Tp(GameObject other)
    {
        SceneManager.LoadScene(targetScene.name);
    }
}
    