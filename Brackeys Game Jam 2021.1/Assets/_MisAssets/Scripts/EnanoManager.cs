using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnanoManager : Singleton<EnanoManager>
{
    public List<Enano> enanos = new List<Enano>();

    private PlayerDirection direction = PlayerDirection.none;

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

}
