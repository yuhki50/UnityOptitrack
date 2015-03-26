/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;
using System.Collections;

public class OptiTrackObject : MonoBehaviour
{
	public int rigidbodyIndex;
	public Vector3 positionOffset;
	public Vector3 rotationOffset;
	
	void Start ()
	{

	}

	void Update ()
	{
		Vector3 pos = OptiTrackManager.Instance.getPosition (rigidbodyIndex);
		pos += positionOffset;
		this.transform.position = pos;

		Quaternion rot = OptiTrackManager.Instance.getOrientation (rigidbodyIndex);
		rot *= Quaternion.Euler (rotationOffset);
		this.transform.rotation = rot;
	}
}
