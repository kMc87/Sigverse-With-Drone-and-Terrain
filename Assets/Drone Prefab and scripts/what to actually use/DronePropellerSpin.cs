using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets_1_1_2.CrossPlatformInput;
using UnityStandardAssets_1_1_2.Characters.ThirdPerson;


namespace SIGVerse.Drone
{
    public class DronePropellerSpin : MonoBehaviour
    {
        private float propSpeed = 2255f;
        //when holding space AND selected base hits a collider propellers will come to a stop
        public GameObject basePlate;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {

            checkDirection();
            
        }
        void checkDirection()
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Rotate(0f, 0f, -propSpeed * Time.deltaTime);

            }
            else if (Input.GetKey(KeyCode.Space))
            {
                propSpeed = propSpeed / .8f;
                transform.Rotate(0f, 0f, -propSpeed * Time.deltaTime);
               
            }
            else
            {
                propSpeed = 2255f;
                transform.Rotate(0f, 0f, propSpeed * Time.deltaTime);

            }
        }

    }
}
