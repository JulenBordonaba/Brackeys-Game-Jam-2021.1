using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enano : MonoBehaviour
{
    public RigidBodyData rbData;


    private Rigidbody _rb;


    private void Awake()
    {
        rbData = new RigidBodyData();
        rb = GetComponent<Rigidbody>();
        rbData.SaveData(rb);
    }

    private void Update()
    {
        if(RigidBodyEnabled)
        {
            if(rb==null)
            {
                EnableRigidBody();
            }
        }
        else
        {
            if(rb!=null)
            {
                DisableRigidBody();
            }
        }

        gameObject.tag = IsPlayer ? "Player" : "Floor";

        //if(rb!=null)
        //{
        //    rb.isKinematic = IsAlone;
        //}
    }

    private void EnableRigidBody()
    {
        Rigidbody aux = gameObject.AddComponent<Rigidbody>();
        aux = rbData.SetData(aux);
    }

    private void DisableRigidBody()
    {
        rbData.SaveData(rb);
        Destroy(rb);
    }

    private Rigidbody rb
    {
        get
        {
            if (_rb != null) return _rb;

            return GetComponent<Rigidbody>();
            
        }
        set
        {
            _rb = value;
        }
    }

    public bool IsPlayer
    {
        get
        {
            return EnanoManager.Instance.HasEnano(this);
        }
    }

    public bool IsAlone
    {
        get
        {
            if (!IsPlayer) return true;

            if (EnanoManager.Instance.Count <= 1) return true;

            return false;
        }
    }

    private bool RigidBodyEnabled
    {
        get
        {

            if (IsPlayer) return false;

            return true;
        }
    }

    public void Join()
    {
        rb.isKinematic = true;
    }

    public void JumpDown()
    {
        rb.isKinematic = false;
    }

    public void JumpUp()
    {
        rb.isKinematic = false;
    }

    
}
