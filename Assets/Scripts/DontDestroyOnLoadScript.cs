using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    private static bool _loaded;

    private void Awake()
    {
        if (!_loaded)
        {
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);

            _loaded = true;
        }
    }
}
