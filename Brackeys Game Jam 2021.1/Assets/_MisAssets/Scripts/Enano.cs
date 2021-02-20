using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enano : MonoBehaviour
{
    public RigidBodyData rbData;

    public Collider mainCollider;

    public PhysicMaterial playerPhysicMaterial;
    public PhysicMaterial objectPhysicMaterial;

    public GameObject triggers;

    private Rigidbody _rb;

    public bool canKinematic = true;

    private void Awake()
    {
        rbData = new RigidBodyData();
        rb = GetComponent<Rigidbody>();
        rbData.SaveData(rb);
    }

    public IEnumerator KinematicCooldown(float t)
    {
        canKinematic = false;

        yield return new WaitForSeconds(t);

        canKinematic = true;
    }

    private void Update()
    {
        if(this == EnanoManager.Instance.MainEnano)
        {
            if (!triggers.activeInHierarchy)
            {
                triggers.SetActive(true);
            }
        }
        else
        {
            if (triggers.activeInHierarchy)
            {
                triggers.SetActive(false);
            }
        }

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

        mainCollider.material = IsPlayer ? playerPhysicMaterial : objectPhysicMaterial;

        if (canKinematic)
        {
            if (rb)
            {
                if(rb.velocity.magnitude <=0.01f)
                {
                    StartCoroutine(Kinematice());
                }
                
            }
        }


    }

    public void EnableRigidBody()
    {
        Rigidbody aux = gameObject.AddComponent<Rigidbody>();
        aux = rbData.SetData(aux);
    }

    public void DisableRigidBody()
    {
        rbData.SaveData(rb);
        Destroy(rb);
    }

    public Rigidbody rb
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
        DisableRigidBody();
        
    }

    public void JumpDown()
    {
        EnableRigidBody();
    }

    public void JumpUp()
    {
        EnableRigidBody();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (canKinematic)
        //{
        //    if (rb)
        //    {
        //        StartCoroutine(Kinematice());
        //    }
        //}
    }

    IEnumerator Kinematice()
    {
        yield return new WaitForSeconds(0.1f);
        if (rb == null) yield break;

        rb.isKinematic = true;
    }

}
