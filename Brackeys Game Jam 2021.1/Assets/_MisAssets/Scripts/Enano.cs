using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enano : MonoBehaviour
{


    private List<Enano> nearEnanos = new List<Enano>();

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.isKinematic = IsKinematic;
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

    private bool IsKinematic
    {
        get
        {

            if (IsAlone) return false;

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

    public Enano JoinEnano
    {
        get
        {
            if (nearEnanos.Count <= 0) return null;

            PlayerDirection dir = EnanoManager.Instance.Direction;

            List<Enano> auxEnanos = GetNearEnanos(dir);

            return NearestEnano(auxEnanos);
            
        }
    }

    public Enano NearestEnano(List<Enano> auxEnanos)
    {
        if (auxEnanos.Count <= 0) return null;
        Enano nearestEnano = auxEnanos[0];
        float nearestDist = Mathf.Infinity;

        foreach(Enano e in auxEnanos)
        {
            float auxDist = Vector3.Distance(e.transform.position, nearestEnano.transform.position);
            if (auxDist < nearestDist)
            {
                nearestEnano = e;
                nearestDist = auxDist;
            }
        }

        return nearestEnano;
    }

    public List<Enano> GetNearEnanos(PlayerDirection dir)
    {
        List<Enano> auxEnanos = new List<Enano>();

        foreach (Enano e in nearEnanos)
        {
            switch (EnanoManager.Instance.Direction)
            {
                case PlayerDirection.left:
                    
                    if(e.transform.position.x<transform.position.x)
                    {
                        auxEnanos.Add(e);
                    }

                    break;
                case PlayerDirection.right:

                    if (e.transform.position.x > transform.position.x)
                    {
                        auxEnanos.Add(e);
                    }
                    break;
                case PlayerDirection.none:
                    return nearEnanos;
                    break;
                default:
                    break;
            }
        }

        if (auxEnanos.Count <= 0) return nearEnanos;

        return auxEnanos;
    }



    private void OnTriggerEnter(Collider other)
    {
        Enano aux;
        if((aux = other.gameObject.GetComponent<Enano>()) != null)
        {
            if(!nearEnanos.Contains(aux))
            {
                nearEnanos.Add(aux);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enano aux;
        if ((aux = other.gameObject.GetComponent<Enano>()) != null)
        {
            if (nearEnanos.Contains(aux))
            {
                nearEnanos.Remove(aux);
            }
        }
    }
}
