﻿/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;
using System;
using System.Collections;
using OptitrackManagement;

public class OptiTrackManager : MonoBehaviour
{
	public string name;
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
		Debug.Log (name + ": Initializing");
		
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
		if (OptitrackManagement.DirectMulticastSocketClient.IsInit ()) {
			DataStream networkData = OptitrackManagement.DirectMulticastSocketClient.GetDataStream ();
			Vector3 pos = origin + networkData.getRigidbody (rigidbodyIndex).position * scale;
			pos.x = -pos.x;
			return pos;
		} else {
			return Vector3.zero;
		}
	}

	public Quaternion getOrientation (int rigidbodyIndex)
	{
		// should add a way to filter it
		if (OptitrackManagement.DirectMulticastSocketClient.IsInit ()) {
			DataStream networkData = OptitrackManagement.DirectMulticastSocketClient.GetDataStream ();
			Quaternion rot = networkData.getRigidbody (rigidbodyIndex).orientation;

			// change the handedness from motive
			rot = new Quaternion (rot.x, -rot.y, -rot.z, rot.w); // depending on calibration
			return rot;
		} else {
			return Quaternion.identity;
		}
	}

	void Update ()
	{

	}

	void Destroy ()
	{
		OptitrackManagement.DirectMulticastSocketClient.Close ();
	}
}
