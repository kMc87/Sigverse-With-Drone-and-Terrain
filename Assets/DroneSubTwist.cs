using UnityEngine;
using SIGVerse.RosBridge;
using SIGVerse.Common;


namespace SIGVerse.Drone
{
	public class DroneSubTwist : RosSubMessage<SIGVerse.RosBridge.geometry_msgs.Twist>
	{

		//--------------------------------------------------
		public Rigidbody baseRigidbody;

		DroneController dc;

		private float linearVelX;
		private float angularVelZ;
		
		private bool isMoving = false;

		void Awake()
		{
			dc = FindObjectOfType<DroneController>();
			
			//this.baseFootprint = TurtleBot3Common.FindGameObjectFromChild(this.transform.root, TurtleBot3LinkInfo.LinkType.BaseFootprint);

			//this.baseRigidbody = this.baseFootprint.GetComponent<Rigidbody>();
		}

		protected override void SubscribeMessageCallback(RosBridge.geometry_msgs.Twist twist)
		{
			dc.forward();
			/*
			if(twist.linear.x>0)
            {
				dc.forward();
				Debug.Log("forward");
			}
			else if(twist.linear.x<0)
            {
				dc.backward();
            }
			*.
			/*
			float linearVel = Mathf.Sqrt(Mathf.Pow(twist.linear.x, 2) + Mathf.Pow(twist.linear.y, 2));

			float linearVelClamped = Mathf.Clamp(linearVel, 0.0f, TurtleBot3Common.MaxSpeedBase);

			if(linearVel >= 0.001)
			{
				this.linearVelX  = twist.linear.x * linearVelClamped / linearVel;
			}
			else
			{
				this.linearVelX = 0.0f;
			}

			this.angularVelZ = Mathf.Sign(twist.angular.z) * Mathf.Clamp(Mathf.Abs(twist.angular.z), 0.0f, TurtleBot3Common.MaxSpeedBaseRad);

//			Debug.Log("linearVel=" + linearVel + ", angularVel=" + angularVel);
			this.isMoving = Mathf.Abs(this.linearVelX) >= 0.001f || Mathf.Abs(this.angularVelZ) >= 0.001f;
			*/
		}

		void FixedUpdate()
		{
			Debug.Log("correct");

			/*
			if (Mathf.Abs(this.baseFootprint.forward.y) < wheelInclinationThreshold) { return; }

			if (!this.isMoving) { return; }

			UnityEngine.Vector3 deltaPosition = (-this.baseFootprint.right * linearVelX) * Time.fixedDeltaTime;
			this.baseRigidbody.MovePosition(this.baseFootprint.position + deltaPosition);

			Quaternion deltaRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -angularVelZ * Mathf.Rad2Deg * Time.fixedDeltaTime));
			this.baseRigidbody.MoveRotation(this.baseRigidbody.rotation * deltaRotation);
			*/
		}
	}
}

