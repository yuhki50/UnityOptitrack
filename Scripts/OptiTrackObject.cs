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
	public Vector3 LocalPositionOffset;
	public Vector3 LocalRotationOffset;

	void Start ()
	{

	}

	void Update ()
	{
		var position = OptiTrackManager.Instance.getPosition (RigidbodyIndex);
		var rotation = OptiTrackManager.Instance.getOrientation (RigidbodyIndex);

		var isTracked = position == OptiTrackManager.Instance.origin;  //FIXME
		if (isTracked) {
			return;
		}

		rotation *= Quaternion.Euler (RotationOffset);
		this.transform.rotation = rotation;

		position += PositionOffset;
		this.transform.position = position;

		this.transform.Translate(LocalPositionOffset);
		this.transform.Rotate(LocalRotationOffset);
	}
}
