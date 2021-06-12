using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject currentGo;

    
    public void SpawnObject()
    {
        currentGo = Instantiate(prefabToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGo == null)
        {
            SpawnObject();
        }
    }
}
