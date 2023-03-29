using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets_1_1_2.CrossPlatformInput;
using UnityStandardAssets_1_1_2.Characters.ThirdPerson;


namespace SIGVerse.Drone
{
	public class DroneController : MonoBehaviour
	{

		public Rigidbody Drone;

		public GameObject DroneMarkers;
		public bool auto = false;
		public float autoHeight = 50;

		DronePropellerSpin propellerSpin;


		public int DirectionalSpeed = 25;
		public int Tilt = 25;
		public int rotationSpeed = 5;
		public int LiftSpeed = 14;
		private bool moving;
		public bool landing;
		public bool bTakePhotos = false;

		int count = 0;
		private Vector3 DroneRotation;

        private void Start()
        {
        }

        void Update()
		{
			count ++;
		}
		//rather than using update which updates every frame, fixed update runs at a fixed interval
		//this means taht it can update multiple times per frame
		private void FixedUpdate()
		{
			DroneRotation = Drone.transform.localEulerAngles;//provides angle relative to parent transforms rotation

			TiltCorrection();

			if (auto)
				DroneAuto();
			else
				DroneControls();

			if (Input.GetKey(KeyCode.K))
			{
				bTakePhotos = !bTakePhotos;
				if (bTakePhotos)
					InvokeRepeating("screenshotter", 0, 1);
				else
					CancelInvoke("screenshotter");
			}

		}
		void TiltCorrection()
        {
			#region tilt correction
			//if tilt too big(stabilizes drone on z-axis)
			if (DroneRotation.z > 10 && DroneRotation.z <= 180)
			{
				Drone.AddRelativeTorque(0, 0, -10);
			}
			if (DroneRotation.z > 180 && DroneRotation.z <= 350)
			{
				Drone.AddRelativeTorque(0, 0, 10);
			}

			//if tilt too small(stabilizes drone on z-axis)
			if (DroneRotation.z > 1 && DroneRotation.z <= 10)
			{
				Drone.AddRelativeTorque(0, 0, -3);
			}
			if (DroneRotation.z > 350 && DroneRotation.z <= 359)
			{
				Drone.AddRelativeTorque(0, 0, 3);
			}

			/*if (!moving)
			{
				//tilt drone left
				if (Input.GetKey(KeyCode.A))
				{
					Drone.AddRelativeTorque(0, 0, 0);
				}
				//tilt drone right
				if (Input.GetKey(KeyCode.D))
				{
					Drone.AddRelativeTorque(0, 0, 0);
				}
			}*/

			//if tilt too big(stabilizes drone on x-axis)
			if (DroneRotation.x > 10 && DroneRotation.x <= 180)
			{
				Drone.AddRelativeTorque(-10, 0, 0);
			}
			if (DroneRotation.x > 180 && DroneRotation.x <= 350)
			{
				Drone.AddRelativeTorque(10, 0, 0);
			}

			//if tilt too small(stabilizes drone on x-axis)
			if (DroneRotation.x > 1 && DroneRotation.x <= 10)
			{
				Drone.AddRelativeTorque(-3, 0, 0);
			}
			if (DroneRotation.x > 350 && DroneRotation.x <= 359)
			{
				Drone.AddRelativeTorque(3, 0, 0);
			}


			#endregion
		}

		void DroneControls()
		{
			landing = false;

			
			#region drone controls
			if (!landing)
			{

				Drone.AddForce(0, 8f, 0);//keeps the drone from losing height quickly, since the drone is set to a rigidbody itll fall at 9.8 until it hits a collider
										 //9.8f(f makes it a float rather than a double

				if (moving == false)
				{
					if (Input.GetKey(KeyCode.W))
					{
						forward();
					}
					if (Input.GetKey(KeyCode.S))
					{
						backward();
					}
					//left
					if (Input.GetKey(KeyCode.A))
					{
						leftward();

					}
					//right
					if (Input.GetKey(KeyCode.D))
					{
						rightward();
					}
					//lift
					if (Input.GetKey(KeyCode.UpArrow))
					{
						lift();
					}
					//drop
					if (Input.GetKey(KeyCode.DownArrow))
					{
						dropper();
					}
					//rotate left
					if (Input.GetKey(KeyCode.LeftArrow))
					{
						TiltLeft();
					}
					//rotate right
					if (Input.GetKey(KeyCode.RightArrow))
					{
						TiltRight();
					}
					//call land funciton
					if (Input.GetKeyDown(KeyCode.Space))
					{
						lander();
					}

				}
			
			}
            else
            {
				if (propellerSpin.propSpeed < 2255f)
				{
					if (propellerSpin.propSpeed == 0)
					{
						Drone.AddForce(0, (-9.8f)*Time.deltaTime, 0);//keeps the drone from losing height quickly, since the drone is set to a rigidbody itll fall at 9.8 until it hits a collider
													//9.8f(f makes it a float rather than a double
					}
					else
					{
						for (int i = 8; i > 0; i--)
						{
							Drone.AddForce(0, i, 0);//keeps the drone from losing height quickly, since the drone is set to a rigidbody itll fall at 9.8 until it hits a collider
													//9.8f(f makes it a float rather than a double
						}
					}
				}

			}

			#endregion
		}

		//"Ai" controller. Edit this if you want to increase autonomous capabilities
		void DroneAuto()
		{
			/*look for number of children the DroneMarkers has
			 * then create a for loop for the index to cycle through each
			 * marker*/
			/*int numChildren = DroneMarkers.transform.childCount;
			for (int i = 0; i < numChildren; i++)
			{
			Transform child = DroneMarkers.transform.GetChild(i);
			// Do something with the child object...
			}*/

			int numChildren = DroneMarkers.transform.childCount;
			
			for (int i = 0; i < numChildren; i++)
			{
				//Transform child = DroneMarkers.transform.GetChild(i);
				// Do something with the child object...
				Vector3 loc = DroneMarkers.transform.GetChild(i).position;  //Vector3 loc = DroneMarkers.transform.GetChild("index").position;
				float dist = Math.Abs(loc.z - transform.position.z);

				if (transform.position.y < autoHeight)
				{
					lift();
				}
				else
				{
					if (dist > 10)
					{
						// Calculate the direction vector towards the target location
						Vector3 direction = (loc - transform.position).normalized;
						direction.y = 0; // make sure the drone stays level

						// Rotate the drone towards the target direction
						Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0);
						transform.rotation = Quaternion.LookRotation(newDirection);

						// Move the drone forward in the target direction
						forward();
					}
					
				}
			}
		}


		//forward
		public void forward()
        {

			Drone.AddRelativeForce(0, 0, DirectionalSpeed*10);
			Drone.AddRelativeTorque(10, 0, 0);

		}

		//backward
		public void backward()
        {

			Drone.AddRelativeForce(0, 0, -DirectionalSpeed*10);
			Drone.AddRelativeTorque(-10, 0, 0);

		}

		//strafe left
		public void leftward()
        {

				Drone.AddRelativeForce(-DirectionalSpeed*10, 0, 0);
				Drone.AddRelativeTorque(0, 0, 15);

		}

		//strafe right
		public void rightward()
        {

				Drone.AddRelativeForce(DirectionalSpeed*10, 0, 0);
				Drone.AddRelativeTorque(0, 0, -15);

			
		}

		//goes up
		public void lift()
        {

				Drone.AddRelativeForce(0, LiftSpeed*10, 0);

			
		}

		//goes down
		public void dropper()
        {

				Drone.AddRelativeForce(0, -LiftSpeed*10, 0);

			
		}

		//turns left
		public void TiltLeft()
        {

				transform.Rotate(0f, -rotationSpeed, 0f, Space.Self);

			
		}

		//turns right
		public void TiltRight()
        {

				transform.Rotate(0f, rotationSpeed, 0f, Space.Self);

			
		}

		public void lander()
        {

				//call land and set land bool to true so that you just descend from the selected location
				//land();
				Drone.AddRelativeForce(0, (-LiftSpeed) * Time.deltaTime, 0);

			
		}

		public void screenshotter()
        {

				ScreenshotHandler.takeScreenShot_Static(500, 500);
			
		}

#region landing
void land()
        {
			landing = true;


			//drop the drone at a slowish speed
			Drone.AddForce(0, (-LiftSpeed)* Time.deltaTime, 0);

			//stop landing
			if (Input.GetKeyUp(KeyCode.Space))
			{
				landing = false;
			}
			
		}

        #endregion
    }
}
