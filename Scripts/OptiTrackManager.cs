/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;
using System;
using System.Collections;
using OptitrackManagement;

public class OptiTrackManager : MonoBehaviour
{
	public float scale = 1f;
	private static OptiTrackManager instance;
	public Vector3 origin = Vector3.zero; // set this to wherever you want the center to be in your scene

	public static OptiTrackManager Instance {
		get { return instance; } 
	}

	void Awake ()
	{
		instance = this;
	}

	~OptiTrackManager ()
	{      
		Debug.Log ("OptitrackManager: Destruct");
		OptitrackManagement.DirectMulticastSocketClient.Close ();
	}

	void Start ()
	{
		OptitrackManagement.DirectMulticastSocketClient.Start ();
		Application.runInBackground = true;
	}

	public OptiTrackRigidBody getOptiTrackRigidBody (int index)
	{
		// only do this if you want the raw data
		if (OptitrackManagement.DirectMulticastSocketClient.IsInit ()) {
			DataStream networkData = OptitrackManagement.DirectMulticastSocketClient.GetDataStream ();
			return networkData.getRigidbody (index);
		} else {
			OptitrackManagement.DirectMulticastSocketClient.Start ();
			return getOptiTrackRigidBody (index);
		}
	}

	public Vector3 getPosition (int rigidbodyIndex)
	{
		Vector3 pos;

		if (OptitrackManagement.DirectMulticastSocketClient.IsInit ()) {
			DataStream networkData = OptitrackManagement.DirectMulticastSocketClient.GetDataStream ();
			pos = networkData.getRigidbody (rigidbodyIndex).position;
		} else {
			pos = Vector3.zero;
		}

		return adjustPosition (pos);
	}

	public Quaternion getOrientation (int rigidbodyIndex)
	{
		// should add a way to filter it
		if (OptitrackManagement.DirectMulticastSocketClient.IsInit ()) {
			DataStream networkData = OptitrackManagement.DirectMulticastSocketClient.GetDataStream ();
			Quaternion rot = networkData.getRigidbody (rigidbodyIndex).orientation;

			// change the handedness from motive
			rot = new Quaternion (rot.x, -rot.y, -rot.z, rot.w);
			return  rot;
		} else {
			return Quaternion.identity;
		}
	}

	public bool getTracked (int rigidbodyIndex)
	{
		return getPosition (rigidbodyIndex) != adjustPosition (Vector3.zero);
	}

	private Vector3 adjustPosition (Vector3 pos)
	{
		pos += origin;
		pos *= scale;
		pos.x = -pos.x;

		return pos;
	}

	void Update ()
	{

	}

	void Destroy ()
	{
		OptitrackManagement.DirectMulticastSocketClient.Close ();
	}
}
