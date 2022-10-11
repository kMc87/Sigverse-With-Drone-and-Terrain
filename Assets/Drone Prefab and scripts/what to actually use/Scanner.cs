using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets_1_1_2.CrossPlatformInput;
using UnityStandardAssets_1_1_2.Characters.ThirdPerson;


namespace SIGVerse.Drone
{
    public class Scanner : MonoBehaviour
    {
        //x axis is left to right
        //y moves up and down, just do this on the front scanners

        public bool leftToRight;
        public bool RightToLeft;
        public bool UptoDown;
        public bool DowntoUp;
        bool rotateX;
        bool rotateY;

        Quaternion angle;
        float MoveAngle;
        float Scanspeed;

         void Start()
        {
            MoveAngle = 0;
            Scanspeed = 3;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            scanning();
        }

        void scanning()
        {
            transform.rotation = angle;
            if (leftToRight || RightToLeft)
            {
                rotateX = true;
                rotateY = false;
            }
            else
            {
                rotateX = false;
                rotateY = true;
            }

            if (rotateX == true)
            {
                if (leftToRight)
                {
                    MoveAngle -= Time.deltaTime * Scanspeed;

                    if (MoveAngle <= -45)
                    {
                        leftToRight = !leftToRight;
                        RightToLeft = !RightToLeft;
                    }

                }
                else
                {
                    MoveAngle += Time.deltaTime * Scanspeed;

                    if (MoveAngle <= 45)
                    {
                        RightToLeft = !RightToLeft;
                        leftToRight = !leftToRight;
                    }
                }

                transform.localRotation = Quaternion.Euler(MoveAngle, 0, 0);
            }
            else
            {
                if (UptoDown)
                {
                    MoveAngle -= Time.deltaTime * Scanspeed;

                    if (MoveAngle <= -45)
                    {
                        UptoDown = !UptoDown;
                        DowntoUp = !DowntoUp;
                    }
                }
                else 
                {
                    MoveAngle += Time.deltaTime * Scanspeed;

                    if (MoveAngle <= 45)
                    {
                        UptoDown = !UptoDown;
                        DowntoUp = !DowntoUp;
                    }
                }
                transform.localRotation = Quaternion.Euler(0, 0, MoveAngle);
            }
        }
    }
}