/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;
using System.Collections;

public class OptiTrackObject : MonoBehaviour
{
	public int rigidbodyIndex;
	public Vector3 rotationOffset;
	
	void Start ()
	{

	}

	void Update ()
	{
		Vector3 pos = OptiTrackManager.Instance.getPosition (rigidbodyIndex);
		Quaternion rot = OptiTrackManager.Instance.getOrientation (rigidbodyIndex);
		rot = rot * Quaternion.Euler (rotationOffset);
		this.transform.position = pos;
		this.transform.rotation = rot;
	}
}
