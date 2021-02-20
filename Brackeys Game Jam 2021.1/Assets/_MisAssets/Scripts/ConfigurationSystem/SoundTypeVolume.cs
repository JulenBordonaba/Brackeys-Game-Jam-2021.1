using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTypeVolume : MonoBehaviour
{
    public AudioSource audioSource;

    public SoundType soundType;

    [HideInInspector]
    public List<Modifier> soundModifiers = new List<Modifier>();

    private float defaultVolume;



    private void Awake()
    {
        

        if(audioSource==null)
        {
            if(GetComponent<AudioSource>()!=null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }

        defaultVolume = audioSource.volume;
    }
    
    // Update is called once per frame
    void Update()
    {
        audioSource.volume = FinalVolume;
    }

    public float FinalVolume
    {
        get
        {
            if(soundType== SoundType.effect)
            {
                return defaultVolume * ConfigurationManager.settings.effectVolume * ConfigurationManager.settings.generalVolume * FinalMultiplier;
            }
            else if (soundType== SoundType.music)
            {
                return defaultVolume * ConfigurationManager.settings.musicVolume * ConfigurationManager.settings.generalVolume * FinalMultiplier;
            }
            return defaultVolume;
        }
    }

    public void SetModifierValue(string id, float m_value)
    {
        if(GetModifierById(id)!=null)
        {
            GetModifierById(id).Multiplier = m_value;
        }
    }

    private Modifier GetModifierById(string id)
    {
        if (soundModifiers.Count <= 0) return null;


        foreach (Modifier modifier in soundModifiers)
        {
            if(modifier.Id==id)
            {
                return modifier;
            }
        }

        return null;
    }

    private float FinalMultiplier
    {
        get
        {
            float n = 1;
            if(soundModifiers.Count>0)
            {
                foreach (Modifier modifier in soundModifiers)
                {
                    n *= modifier.Multiplier;
                }
            }
            

            return n;
        }
    }
}
