using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropPlanter : MonoBehaviour
{

    public GameObject crop1, crop2, crop3, crop4;
    GameObject set;
    public float constraint1, constrain2, constraint3, max;
    private float currentFertLevel;
    bool fertilized;
    private float TimeSinceFertilized, tempTime;

    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        crop1.SetActive(true);
        crop2.SetActive(true);
        crop3.SetActive(true);
        crop4.SetActive(true);

        GameObject set = crop1;
        Instantiate(crop1, gameObject.transform.position, Quaternion.identity);
        if (test == true)
        {
            fertilized = true;
        }
    }




    // Update is called once per frame
    void Update()
    {

        if(fertilized == true)
        {
            TimeSinceFertilized += Time.deltaTime;
            if (TimeSinceFertilized <= 35)
            {
                Debug.Log("Crop: " + gameObject.name + " is growing");
                currentFertLevel += Time.deltaTime;
                if (currentFertLevel >= constraint1 && currentFertLevel <= constrain2)
                {
                    ConvertUp(crop1, crop2);
                }
                if (currentFertLevel >= constrain2 && currentFertLevel <= constraint3)
                {
                    ConvertUp(crop2, crop3);
                }
                if (currentFertLevel >= constraint3)
                {
                    ConvertUp(crop3, crop4);
                }
                if (currentFertLevel >= max)
                {
                    currentFertLevel = max - 1;
                }
            }
            else
            {
                fertilized = false;
                currentFertLevel -= Time.deltaTime;

                tempTime += Time.deltaTime;
                if (tempTime <= 35)
                {
                    Debug.Log("Crop " + gameObject.name + " has died ");
                    Death(set);
                }
            }
        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Fertilizer")
        {
            fertilized = true;
        }
        else
        {
            fertilized = false;
        }
    }


    void Death(GameObject RIPSoulja)
    {
        Destroy(RIPSoulja);
        Debug.Log("Rest Easy King");
    }

    void ConvertUp(GameObject current, GameObject desired)
    {
        Destroy(current);
        Instantiate(desired, gameObject.transform.position, Quaternion.identity);
    }

}
