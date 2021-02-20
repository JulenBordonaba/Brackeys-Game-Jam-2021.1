using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = "Controls/JumpData", order = 1)]
public class JumpData : ScriptableObject
{
    #region fields

    [SerializeField]
    protected JumpDataInputMode dataType = JumpDataInputMode.VelocityGravity;
    [SerializeField]
    [Tooltip("Velocidad vertical que se aplica para iniciar el salto")]
    protected float initialVelocity;
    [SerializeField]
    [Tooltip("Aceleración vertical que se le aplica al objeto de normal")]
    protected float baseGravity;
    [SerializeField]
    [Tooltip("Aceleración vertical que se le aplica al objeto cuando se ha soltado la tecla de salto")]
    protected float keyReleaseGravity;
    [SerializeField]
    [Tooltip("Aceleración vertical que se le aplica al objeto cuando está cayendo")]
    protected float fallGravity;
    [SerializeField]
    protected float maxHeight;
    [SerializeField]
    protected float timeToMaxHeight;
    [SerializeField]
    protected bool overrideJumpKey = false;
    [SerializeField]
    protected KeyCode jumpKey = KeyCode.None;

    

    #endregion

    #region Properties

    public float InitialVelocity
    {
        get
        {
            return initialVelocity;
        }
        set
        {
            initialVelocity = value;
        }
    }

    public JumpDataInputMode DataType
    {
        get
        {
            return dataType;
        }
        set
        {
            dataType = value;
        }
    }

    public float BaseGravity
    {
        get
        {
            return baseGravity;
        }
        set
        {
            baseGravity = value;
        }
    }

    public float KeyReleaseGravity
    {
        get
        {
            return keyReleaseGravity;
        }
        set
        {
            keyReleaseGravity = value;
        }
    }

    public float FallGravity
    {
        get
        {
            return fallGravity;
        }
        set
        {
            fallGravity = value;
        }
    }

    public float MaxHeight
    {
        get
        {
            return maxHeight;
        }
        set
        {
            maxHeight = value;
        }
    }

    public float TimeToMaxHeight
    {
        get
        {
            return timeToMaxHeight;
        }
        set
        {
            timeToMaxHeight = value;
        }
    }

    public bool OverrideJumpKey
    {
        get
        {
            return overrideJumpKey;
        }
        set
        {
            overrideJumpKey = value;
        }
    }

    public KeyCode JumpKey
    {
        get
        {
            return jumpKey;
        }
        set
        {
            jumpKey = value;
        }
    }

    #endregion

}
