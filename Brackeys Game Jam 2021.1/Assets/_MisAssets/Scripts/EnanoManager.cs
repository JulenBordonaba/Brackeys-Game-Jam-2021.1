using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnanoManager : Singleton<EnanoManager>
{
    public List<Enano> enanos = new List<Enano>();

    private PlayerDirection direction = PlayerDirection.none;


    public List<Enano> nearEnanos = new List<Enano>();

    public PlayerDirection Direction
    {
        get
        {
            return direction;
        }
        set
        {
            PlayerDirection aux = value;

            if (aux != direction)
            {
                direction = aux;
                Vector3 scale = Vector3.one;
                switch (direction)
                {
                    case PlayerDirection.left:
                        scale  = transform.localScale;
                        SetScale(new Vector3(Mathf.Abs(scale.x * -1), scale.y, scale.z));
                        break;
                    case PlayerDirection.right:
                        scale = transform.localScale;
                        SetScale(new Vector3(Mathf.Abs(scale.x * 1), scale.y, scale.z));
                        break;
                    case PlayerDirection.none:
                        
                        break;
                    default:
                        break;
                }

            }
        }
    }

    private void SetScale(Vector3 scale)
    {

    }

    public int Count
    {
        get
        {
            return enanos.Count;
        }
    }

    public bool HasEnano(Enano enano)
    {
        return enanos.Contains(enano);
    }

    public Enano MainEnano
    {
        get
        {
            if (enanos.Count <= 0) return null;
            return enanos[0];
        }
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

        foreach (Enano e in auxEnanos)
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

                    if (e.transform.position.x < transform.position.x)
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
        if ((aux = other.gameObject.GetComponent<Enano>()) != null)
        {
            if (!nearEnanos.Contains(aux))
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
