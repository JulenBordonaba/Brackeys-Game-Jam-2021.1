using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RigidBodyData 
{
    public Vector3 velocity;
    public Vector3 position;
    public Quaternion rotation;
    public RigidbodyConstraints constraints;
    public RigidbodyInterpolation interpolation;
    public CollisionDetectionMode collisionDetectionMode;
    public bool isKinematic;
    public bool useGravity;

    public void SaveData(Rigidbody rb)
    {
        velocity = rb.velocity;
        position = rb.position;
        rotation = rb.rotation;
        constraints = rb.constraints;
        interpolation = rb.interpolation;
        collisionDetectionMode = rb.collisionDetectionMode;
        isKinematic = rb.isKinematic;
        useGravity = rb.useGravity;
    }

    public Rigidbody SetData(Rigidbody rb)
    {
        rb.velocity = velocity;
        rb.position = position;
        rb.rotation = rotation;
        rb.constraints = constraints;
        rb.interpolation = interpolation;
        rb.collisionDetectionMode = collisionDetectionMode;
        rb.isKinematic = isKinematic;
        rb.useGravity = useGravity;

        return rb;
    }

}
