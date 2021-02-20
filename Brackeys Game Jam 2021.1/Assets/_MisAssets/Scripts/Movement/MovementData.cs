using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MovementData", menuName = "Controls/MovementData", order = 1)]
public class MovementData : ScriptableObject
{
    public float maxVelocity;
    public float accelerationTime;
    public float decelerationTime;
    public AnimationCurve accelerationCurve;
    public AnimationCurve decelerationCurve;


    public float AccelerationTimeMultiplier
    {
        get
        {
            float curveTime = accelerationCurve[accelerationCurve.length - 1].time;

            //Debug.Log("CurveTime: " + curveTime);

            float mul = (1f / curveTime) * accelerationTime;

            return mul;
        }
    }

    public float AccelerationVelocityMultiplier
    {
        get
        {
            float maxCurveVel = accelerationCurve[accelerationCurve.length - 1].value;

            float mul = (1f/maxCurveVel) *maxVelocity;

            return mul;
        }
    }

    public float DecelerationTimeMultiplier
    {
        get
        {
            float curveTime = accelerationCurve[0].time;

            float mul = (1f / curveTime) * decelerationTime;

            return mul;
        }
    }

    public float DecelerationVelocityMultiplier
    {
        get
        {
            float maxCurveVel = accelerationCurve[0].value;

            float mul = (1f / maxCurveVel) * maxVelocity;

            return mul;
        }
    }
}
