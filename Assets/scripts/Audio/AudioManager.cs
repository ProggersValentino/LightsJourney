 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Random = UnityEngine.Random;

 public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioUnits> SFX;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playSFX(int index)
    {
        int randClip = Random.Range(0, SFX[index].audio.Length);
        AudioClip clipSele = SFX[index].audio[randClip];
        
        SFX[index].unitsSource.PlayOneShot(clipSele);

    }
    
    
}

 [Serializable]
 public class AudioUnits
 {
     public string AudioType;
     public GameObject unit;
     public AudioSource unitsSource;
     
     public AudioClip[] audio;
     
 }