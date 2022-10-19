using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIGVerse.Drone
{
    public class DroneRayCast : MonoBehaviour
    {
        public float height;
        //public Field field;
        RaycastHit hit;
        public Spawner spawn;

        // Update is called once per frame
        void Update()
        {
            Ray ray = new Ray(transform.position, -Vector3.up);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Field")
                {

                    Field other = (Field)hit.collider.gameObject.GetComponent(typeof(Field));
                    
                    if(other == null)
                        other = (Field)hit.collider.gameObject.transform.parent.gameObject.GetComponent(typeof(Field));
                    
                    if(!other.bIsPlanted)
                    {
                        other.bIsPlanted = true;
                        spawn.spawnSeed();
                    }
                }
            }
        }
    }

}

