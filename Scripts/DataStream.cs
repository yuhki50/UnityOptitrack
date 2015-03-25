/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OptitrackManagement
{
	public class DataStream
	{
		private const int RigidBodyMaxLength = 200;
		public OptiTrackRigidBody[] _rigidBody = new OptiTrackRigidBody[RigidBodyMaxLength];
		public int _nRigidBodies = 0;

		public DataStream ()
		{
			InitializeRigidBody ();
		}
		
		public bool InitializeRigidBody ()
		{
			_nRigidBodies = 0;
			for (int i = 0; i < RigidBodyMaxLength; i++) {
				_rigidBody [i] = new OptiTrackRigidBody ();
			}
			return true;
		}

		public OptiTrackRigidBody getRigidbody (int index)
		{
			return _rigidBody [index];
		}
	}
}
