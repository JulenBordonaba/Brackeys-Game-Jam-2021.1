using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanic : MonoBehaviour
{
    [SerializeField]
    protected bool allowed = true;


    public bool Allowed
    {
        get
        {
            return allowed;
        }
        set
        {
            allowed = value;
        }
    }
}
