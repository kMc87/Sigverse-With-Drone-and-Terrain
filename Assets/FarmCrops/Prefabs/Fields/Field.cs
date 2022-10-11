using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public float Health = 10;
    public float HealthRate = 1;
    public float Fertilizer = 0;
    public float FertilizerRate = 1;
    public float Tier2Requirement = 50;
    public float Tier3Requirement = 100;
    public float Tier4Requirement = 200;

    int Stage = 0;
    List<GameObject> Crops = new List<GameObject>();
    public GameObject Tier1Crop, Tier2Crop, Tier3Crop, Tier4Crop;

    public GameObject CropContainer;
   
    // Start is called before the first frame update
    void Start()
    {
        Plant();
    }



    // Update is called once per frame
    void Update()
    {


    }

   void Plant()
    {
        int i = 0;

        for(i=0; i < CropContainer.transform.childCount; i++)
        {
            Vector3 location = CropContainer.transform.GetChild(i).transform.position;

            GameObject newcrop = Instantiate(Tier1Crop, location, Quaternion.identity);

            Crops.Add(newcrop);
        }

        Debug.Log(i);
    }


    

}
