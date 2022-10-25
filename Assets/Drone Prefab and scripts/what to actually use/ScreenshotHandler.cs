using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets_1_1_2.CrossPlatformInput;
using UnityStandardAssets_1_1_2.Characters.ThirdPerson;

namespace SIGVerse.Drone
{
    public class ScreenshotHandler : MonoBehaviour
    {
        private static ScreenshotHandler instance;

        public Camera droneScanner;
        private bool takeScreenshotOnNextFrame;
        int count;
        string ID;
        DateTime dt;

        private void Awake()
        {
           

            instance = this;
            droneScanner = gameObject.GetComponent<Camera>();
        }
        private void Update()
        {
            
            dt = DateTime.Now;
            string name = count.ToString();

            ID = name;
            
        }
        private void OnPostRender()
        {
            count++;


            if (takeScreenshotOnNextFrame)
            {
                takeScreenshotOnNextFrame = false;
                RenderTexture rednerTexture = droneScanner.targetTexture;

                Texture2D renderResult = new Texture2D(rednerTexture.width, rednerTexture.height, TextureFormat.ARGB32, false);
                Rect rect = new Rect(0, 0, rednerTexture.width, rednerTexture.height);
                renderResult.ReadPixels(rect, 0, 0);

                string names = "/ScreenShots/CameraScreenshot{"+ID+"}.png";
                byte[] byteArray = renderResult.EncodeToPNG();
                System.IO.File.WriteAllBytes(Application.dataPath + names, byteArray);

                RenderTexture.ReleaseTemporary(rednerTexture);
                droneScanner.targetTexture = null;
            }
        }

        private void TakeScreenshot(int width, int height)
        {
            droneScanner.targetTexture = RenderTexture.GetTemporary(width, height, 16);
            takeScreenshotOnNextFrame = true;
        }

        public static void takeScreenShot_Static(int width, int height)
        {
            instance.TakeScreenshot(width, height);
        }

    }
}
