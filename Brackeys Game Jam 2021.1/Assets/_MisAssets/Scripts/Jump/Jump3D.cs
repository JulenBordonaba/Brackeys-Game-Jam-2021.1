using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump3D : Mechanic
{
    #region fields

    private List<int> colliderNumbers = new List<int>();
    public float inputSaveTime = 0.2f;

    private float jumpCooldown = 0.1f;
    private bool inCooldown = false;

    private KeyCode nextKey;

    protected float inputPassedTime = 0;

    protected bool hasInput = false;
    #region Main Data
    //-------------------------------------------------------------------------

    //The base jump data
    [SerializeField]
    private JumpData jumpData;
    //The Key that triggers the Jump
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    //Relative sapce to the jump [local/global] (Not implemented)
    [SerializeField]
    private Space relativeSpace;
    [SerializeField]
    private LayerMask floorLayers;

    //-------------------------------------------------------------------------
    #endregion

    #region Extra Data
    //-------------------------------------------------------------------------

    //Allow extra options
    [SerializeField]
    private bool allowExtraOptions = false;
    //Extra Jumps
    [SerializeField]
    private bool allowExtraJumps = false;
    [SerializeField]
    private JumpData[] extraJumps;
    //Chain jumps
    [SerializeField]
    private bool allowChainJumps = false;
    [SerializeField]
    private JumpData[] chainJumps;

    //-------------------------------------------------------------------------
    #endregion

    #region Components
    //-------------------------------------------------------------------------

    private Rigidbody rb;

    //-------------------------------------------------------------------------
    #endregion

    #region Auxiliar Data
    //-------------------------------------------------------------------------

    private int currenteExtraJump = 0;
    private int currentChainJump = 0;
    [HideInInspector]
    public List<Collider> touchingColliders = new List<Collider>();

    //-------------------------------------------------------------------------
    #endregion

    #region Conditions
    //-------------------------------------------------------------------------


    //Añadir condición de salto


    //-------------------------------------------------------------------------
    #endregion

    #endregion

    #region Methods
    //-------------------------------------------------------------------------

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Error al cargar el Rigidbody");
            }
        }
        rb.useGravity = false;
    }

    private void Update()
    {
        InputTimeCount();
        foreach(Collider c in touchingColliders)
        {
            print(c.name);
        }
        //DeleteColliders();
        colliderNumbers = new List<int>();
        if (!allowed) return;
        HandleInput();

        if (hasInput)
        {
            Jump();
        }
    }

    public void SaveInput(KeyCode key)
    {
        hasInput = true;
        nextKey = key;
        inputPassedTime = 0;
    }

    /// <summary>
    /// This function counts the time since the last input and it resets it if it excedes the time
    /// </summary>
    public void InputTimeCount()
    {
        if (!hasInput) return;

        inputPassedTime += Time.deltaTime;

        if (inputPassedTime >= inputSaveTime)
        {
            hasInput = false;
        }
    }

    private void DeleteColliders()
    {
        List<int> noTouchCols = new List<int>();

        for (int i = 0; i < touchingColliders.Count; i++)
        {
            if(!colliderNumbers.Contains(i))
            {
                noTouchCols.Add(i);
            }
        }

        List<Collider> cols = new List<Collider>();
        for (int i = 0; i < noTouchCols.Count; i++)
        {
            cols.Add(touchingColliders[noTouchCols[i]]);
        }

        for (int i = 0; i < cols.Count; i++)
        {
            touchingColliders.Remove(cols[i]);
        }
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(CurrentJumpKey))
        {
            SaveInput(CurrentJumpKey);
        }
    }

    IEnumerator Cooldown(float t)
    {
        inCooldown = true;

        yield return new WaitForSeconds(t);

        inCooldown = false;
    }

    private void SetOnCooldown(float t)
    {
        StartCoroutine(Cooldown(t));
    }

    public void Jump()
    {
        if (!CanJump) return;

        if (inCooldown) return;

        SetOnCooldown(0.3f);

        if (InJump)
        {
            if (AllowExtraJumps)
            {
                if (currenteExtraJump < extraJumps.Length)
                {
                    //we add the initial velocity to our rigidbody
                    SetVelocity(InitialVelocity);

                    currenteExtraJump += 1;
                }
            }
            else
            {
                //Temporal
                //--------------------------------
                //we add the initial velocity to our rigidbody


                if(EnanoManager.Instance.enanos.Count>1)
                {
                    ExtraEnanoJump();
                }
                
                //--------------------------------
            }


        }
        else
        {
            //we add the initial velocity to our rigidbody
            SetVelocity(InitialVelocity);
            hasInput = false;nextKey = KeyCode.None;
        }
    }

    private void ExtraEnanoJump()
    {
        SetVelocity(InitialVelocity);
        EnanoManager.Instance.ThrowMainEnanoDown();
    }

    private void AddVelocity(float velocity)
    {
        //--------------------------

        //variar según el espacio relativo

        //--------------------------


        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + velocity, rb.velocity.z);
    }

    private void SetVelocity(float velocity)
    {
        //--------------------------

        //variar según el espacio relativo

        //--------------------------


        rb.velocity = new Vector3(rb.velocity.x, velocity, rb.velocity.z);
    }

    private void FixedUpdate()
    {
        print(InFloor);
        if (InJump)
        {
            print("Gravity");
            ApplyGravity();
        }
    }

    private void ApplyGravity()
    {
        //--------------------------

        //variar según el espacio relativo

        //--------------------------


        //we apply gravity

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - Gravity * Time.fixedDeltaTime, rb.velocity.z);

    }


    //-------------------------------------------------------------------------
    #endregion

    #region Auxiliar Properties
    //-------------------------------------------------------------------------

    private bool JumpInputPressed
    {
        get
        {
            return Input.GetKey(jumpKey);
        }
    }

    public bool IsFalling
    {
        get
        {
            //--------------------------

            //variar según el espacio relativo

            //--------------------------

            return rb.velocity.y < 0;
        }
    }

    public bool InJump
    {
        get
        {
            return !InFloor;
        }
    }

    public bool CanJump
    {
        get
        {
            return InFloor || EnanoManager.Instance.enanos.Count>1;
        }
    }

    public float InitialVelocity
    {
        get
        {
            return jumpData.InitialVelocity;
        }
    }

    public float Gravity
    {
        get
        {
            if (IsFalling)
            {
                return jumpData.FallGravity; 
            }
            else
            {
                if (JumpInputPressed)
                {
                    return jumpData.BaseGravity; 
                }
                else
                {
                    return Mathf.Clamp(jumpData.KeyReleaseGravity, (-rb.velocity.y - 0.1f) / Time.fixedDeltaTime, (rb.velocity.y + 0.1f) / Time.fixedDeltaTime);
                }
            }
        }
    }

    public JumpData CurrentJump
    {
        get
        {
            if (!AllowExtraJumps)
            {
                return jumpData;
            }

            return extraJumps[currenteExtraJump];
        }
    }

    public bool InFloor
    {
        get
        {
            foreach (Collider collider in touchingColliders)
            {
                if (collider.CompareTag("Floor"))
                {
                    return true;
                }
            }
            return false;
        }
    }

    //-------------------------------------------------------------------------
    #endregion

    #region Properties
    //-------------------------------------------------------------------------

    public bool AllowExtraJumps
    {
        get
        {
            return allowExtraJumps;
        }
        set
        {
            allowExtraJumps = value;
        }
    }

    public bool AllowChainJumps
    {
        get
        {
            return allowChainJumps;
        }
        set
        {
            allowChainJumps = value;
        }
    }

    public bool AllowExtraOptions
    {
        get
        {
            return allowExtraOptions;
        }
        set
        {
            allowExtraOptions = value;
        }
    }

    public JumpData BaseJumpData
    {
        get
        {
            return jumpData;
        }
        set
        {
            jumpData = value;
        }
    }

    public KeyCode CurrentJumpKey
    {
        get
        {
            if (CurrentJump.OverrideJumpKey) return CurrentJump.JumpKey;

            return JumpKey;
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

    public JumpData[] ExtraJumps
    {
        get
        {
            return extraJumps;
        }
        set
        {
            extraJumps = value;
        }
    }

    public JumpData[] ChainJumps
    {
        get
        {
            return chainJumps;
        }
        set
        {
            chainJumps = value;
        }
    }

    public Space RelativeSpace
    {
        get
        {
            return relativeSpace;
        }
        set
        {
            relativeSpace = value;
        }
    }

    public LayerMask FloorLayers
    {
        get
        {
            return floorLayers;
        }
        set
        {
            floorLayers = value;
        }
    }

    //-------------------------------------------------------------------------
    #endregion

    #region Unity Events

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);

        //Gizmos.DrawCube(transform.position, new Vector3(3,3,3));
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);

        //Gizmos.DrawCube(transform.position, new Vector3(3, 3, 3));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!touchingColliders.Contains(other))
        {
            touchingColliders.Add(other);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (touchingColliders.Contains(other))
    //    {
    //        for (int i = 0; i < touchingColliders.Count; i++)
    //        {
    //            if(touchingColliders[i] == other)
    //            {
    //                if(!colliderNumbers.Contains(i))
    //                {
    //                    colliderNumbers.Add(i);
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (touchingColliders.Contains(other))
        {
            touchingColliders.Remove(other);
        }
    }

    #endregion

}
