using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement3D : Mechanic
{
    public MovementData movementData;
    
    private Rigidbody rb;

    private float curveTime = 0;

    private MovementState movementState = MovementState.decelerating;

    private bool anyMovementInputLastUpdate = false;


    #region Input Data

    private float lateralInput = 0;

    #endregion

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Error al cargar el Rigidbody");
            }
        }
        
    }

    private void Update()
    {
        HandleInput();
        CheckMovement();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ManageEnanoManager()
    {
        if(lateralInput<0)
        {
            EnanoManager.Instance.Direction = PlayerDirection.left;
        }

        if (lateralInput > 0)
        {
            EnanoManager.Instance.Direction = PlayerDirection.right;
        }
    }

    private void CheckMovement()
    {
        bool anyMovementInput = AnyMovementInput;

        //if(movementState == MovementState.decelerating)
        //{
        //    if(rb.velocity.x==0)
        //    {
        //        movementState = MovementState.none;
        //    }
        //}

        if (anyMovementInputLastUpdate)
        {
            if (!anyMovementInput)
            {
                StartDecelerating();
            }
        }
        else
        {
            if (anyMovementInput)
            {
                StartAccelerating();
            }
        }
    }

    private void StartDecelerating()
    {
        movementState = MovementState.decelerating;
        float currentVelocity = Mathf.Abs(rb.velocity.x);
        curveTime = GetTimeFromValue(movementData.decelerationCurve, currentVelocity);
    }

    private void StartAccelerating()
    {
        movementState = MovementState.accelerating;
        float currentVelocity = Mathf.Abs(rb.velocity.x);
        curveTime = GetTimeFromValue(movementData.accelerationCurve, currentVelocity);
    }

    private void Accelerate(AnimationCurve curve, float timeMultiplier, float velMultiplier)
    {
        //curveTime += Time.fixedDeltaTime * timeMultiplier;
        curveTime += Time.fixedDeltaTime;

        float newVelX = 0;
        newVelX = curve.Evaluate(curveTime);



        float dir = DirectionMultiplier;



        //Vector3 newVelocity = new Vector3(newVelX * velMultiplier * dir, rb.velocity.y, rb.velocity.z);
        Vector3 newVelocity = new Vector3(newVelX * dir, rb.velocity.y, rb.velocity.z);
        rb.velocity = newVelocity;
    }
    

    private void ApplyMovement()
    {
        float currentVelocity = Mathf.Abs(rb.velocity.x);

        switch(movementState)
        {
            case MovementState.accelerating:
                print(movementData.AccelerationTimeMultiplier);
                Accelerate(movementData.accelerationCurve, 1f/movementData.AccelerationTimeMultiplier, movementData.AccelerationVelocityMultiplier);
                break;
            case MovementState.decelerating:
                Accelerate(movementData.decelerationCurve, 1f / movementData.DecelerationTimeMultiplier, movementData.DecelerationVelocityMultiplier);
                break;
            case MovementState.none:
                break;
            default:
                break;
        }
    }

    private float GetTimeFromValue(AnimationCurve curve, float requieredValue)
    {
        float errorRange = 0.3f;
        int checksPerSecond = 50;

        float duration = curve[curve.length - 1].time;
        
        int checks = Mathf.CeilToInt(duration * checksPerSecond);

        float currentTime = 0;

        float timeIncrease = 1f / (float)checksPerSecond;

        float currentBestTime = currentTime;
        float currentBestDistance = float.MaxValue;

        for (int i = 0; i < checks; i++)
        {
            float currentValue = curve.Evaluate(currentTime);

            float currentDistance = Mathf.Abs(currentValue - requieredValue);

            if (currentDistance < errorRange)
            {
                if(currentDistance<currentBestDistance)
                {
                    currentBestTime = currentTime;
                }
            }
            currentTime += timeIncrease;
            currentTime = Mathf.Clamp(currentTime, 0, duration);
        }

        return currentBestTime;
    }

    private void HandleInput()
    {
        lateralInput = Input.GetAxisRaw("Horizontal");
    }


    private bool AnyMovementInput
    {
        get
        {
            return lateralInput != 0;
        }
    }

    private float DirectionMultiplier
    {
        get
        {
            return lateralInput;
        }
    }
}
