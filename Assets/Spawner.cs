using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objToSpawn;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            spawnSeed();
        }
    }

    public void spawnSeed()
    {
        Instantiate(objToSpawn, transform.position, Quaternion.identity);
    }
}
