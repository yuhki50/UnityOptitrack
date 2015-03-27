/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;

public class OptiTrackObject : MonoBehaviour
{
	public int RigidbodyIndex;
	public Vector3 PositionOffset;	
	public Vector3 RotationOffset;

	void Start ()
	{

	}

	void Update ()
	{
		var position = OptiTrackManager.Instance.getPosition (RigidbodyIndex);
		position += PositionOffset;
		this.transform.position = position;

		var rotation = OptiTrackManager.Instance.getOrientation (RigidbodyIndex);
		rotation *= Quaternion.Euler (RotationOffset);
		this.transform.rotation = rotation;
	}
}
