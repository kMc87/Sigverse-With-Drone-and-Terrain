using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIGVerse.Drone
{
    public class DroneRayCast : MonoBehaviour
    {
        public float height;
        public Field field;
        RaycastHit hit;

        // Update is called once per frame
        void Update()
        {
            Ray ray = new Ray(transform.position, -Vector3.up);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Field")
                {
                    field.bIsPlanted = true;
                    Debug.Log("truee");
                }
            }
        }
    }

}

