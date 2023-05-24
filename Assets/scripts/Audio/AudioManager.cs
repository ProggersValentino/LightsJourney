 using System;
 using System.Collections;
using System.Collections.Generic;
 using UnityEditor.ShaderGraph.Drawing;
 using UnityEngine;
 using Random = UnityEngine.Random;

 public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioUnits> SFX;
    
    //checking to see if audio is playing
    public bool isPlaying;


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
        isPlaying = true;
        if (Time.time > clipSele.length + Time.time)
        {
            isPlaying = false;
        }
    }

    public void soundChecker(int index)
    {
        Debug.Log(isPlaying);
        if (isPlaying)
        {
            return;
        }
        else
        {
            playSFX(index);
        }
        
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