using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[SerializeField]
public class Modifier
{
    private readonly string id;

    private float multiplier;

    public Modifier (string id)
    {
        this.id = id;
    }

    public Modifier(string id, float multiplier)
    {
        this.id = id;
        this.multiplier = multiplier;
    }


    public string Id
    {
        get
        {
            return id;
        }
    }

    public float Multiplier
    {
        get
        {
            return multiplier;
        }
        set
        {
            multiplier = value;
        }
    }
}
