using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public GameObject Tier1Crop, Tier2Crop, Tier3Crop, Tier4Crop;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    //Used to pass crop info to field
    void OnCollisionEnter(Collision collision)
    {
        //If we collided with field
        if (collision.gameObject.tag == "Field")
        {
            //Get a reference to the Field script attatched to the gameObject
            Field other = (Field)collision.gameObject.transform.parent.gameObject.GetComponent(typeof(Field));
            
            //Pass crop references along
            other.Tier1Crop = Tier1Crop;
            other.Tier2Crop = Tier2Crop;
            other.Tier3Crop = Tier3Crop;
            other.Tier4Crop = Tier4Crop;
            
            //Call the plant function to start growth cycle for the field
            other.Plant();

            //Kill ourself
            Destroy(gameObject);
        }
    }

    
}
