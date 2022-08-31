// Generated by gencs from sensor_msgs/TimeReference.msg
// DO NOT EDIT THIS FILE BY HAND!

using System;
using System.Collections;
using System.Collections.Generic;
using SIGVerse.RosBridge;
using UnityEngine;

using SIGVerse.RosBridge.std_msgs;

namespace SIGVerse.RosBridge 
{
	namespace sensor_msgs 
	{
		[System.Serializable]
		public class TimeReference : RosMessage
		{
			public std_msgs.Header header;
			public SIGVerse.RosBridge.msg_helpers.Time time_ref;
			public string source;


			public TimeReference()
			{
				this.header = new std_msgs.Header();
				this.time_ref = new SIGVerse.RosBridge.msg_helpers.Time();
				this.source = "";
			}

			public TimeReference(std_msgs.Header header, SIGVerse.RosBridge.msg_helpers.Time time_ref, string source)
			{
				this.header = header;
				this.time_ref = time_ref;
				this.source = source;
			}

			new public static string GetMessageType()
			{
				return "sensor_msgs/TimeReference";
			}

			new public static string GetMD5Hash()
			{
				return "fded64a0265108ba86c3d38fb11c0c16";
			}
		} // class TimeReference
	} // namespace sensor_msgs
} // namespace SIGVerse.ROSBridge
