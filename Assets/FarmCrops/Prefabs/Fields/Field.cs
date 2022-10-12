using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public float Health = 10;
    public float HealthRate = 10;
    public float Tier2Requirement = 20;
    public float Tier3Requirement = 40;
    public float Tier4Requirement = 80;
    
    public float GrowLoopDelay = 1;
    int Stage = 0;

    List<GameObject> Crops = new List<GameObject>();
    public GameObject Tier1Crop, Tier2Crop, Tier3Crop, Tier4Crop;
    public GameObject CropContainer;

    //TODO: Impliment fertilizer logic maybe
    //public float Fertilizer = 0;
    //public float FertilizerRate = 1;


    // Start is called before the first frame update
    void Start()
    {
        //TODO: Impliment Plant() into a seed collision function
        Plant();
    }


    // Update is called once per frame
    void Update()
    {


    }


    //Initializes field with crops and starts growth cycle
   void Plant()
    {
        //Loop through objects parented to crop container, spawining crop prefabs at their world location. 
        for(int i = 0; i < CropContainer.transform.childCount; i++)
        {
            //Get the location of current crop location marker
            Vector3 location = CropContainer.transform.GetChild(i).transform.position;

            //Spawn the crop prefab
            GameObject newcrop = Instantiate(Tier1Crop, location, Quaternion.identity);

            //Add it to the crop array for later access
            Crops.Add(newcrop);
        }

        //Set stage of growth and start growth loop
        Stage = 1;
        StartCoroutine(StartGrowing());

    }


    //Grow Loop (better performance than update and it's callable, so loop wont start until planted)
    //Increases health in intervals as a way to mimic growth
    IEnumerator StartGrowing()
    {
        //Loop delay
        yield return new WaitForSeconds(GrowLoopDelay);

        //Increase health
        Health += HealthRate;
        Debug.Log(Health);

        //Check if we can grow based on what stage we're at
        switch(Stage)
        {
            case 1:
                if (Health >= Tier2Requirement)
                {
                    Grow(Tier2Crop);
                    Stage++;
                }
                break;
            
            case 2:
                if (Health >= Tier3Requirement)
                {
                    Grow(Tier3Crop);
                    Stage++;
                }
                break;

            case 3:
                if (Health >= Tier4Requirement)
                {
                    Grow(Tier4Crop);
                    Stage++;
                }
                break;

            default:
                break;
        }

        //Loop
        StartCoroutine(StartGrowing());
    }


    //Grow to the next stage of plant life
    void Grow(GameObject NextStage)
    {
        //Loop through crop objects, deleting them. 
        for (int i = 0; i < Crops.Count; i++)
        {
            //Delete the crop prefab
            Destroy(Crops[i]);
        }

        //Ready list for new crops
        Crops.Clear();

        //Loop through objects parented to crop container, spawining crop prefabs at their world location. 
        for (int i = 0; i < CropContainer.transform.childCount; i++)
        {
            //Get the location of current crop location marker
            Vector3 location = CropContainer.transform.GetChild(i).transform.position;

            //Spawn the crop prefab
            GameObject newcrop = Instantiate(NextStage, location, Quaternion.identity);

            //Add it to the crop array for later access
            Crops.Add(newcrop);
        }
    }

    

}
