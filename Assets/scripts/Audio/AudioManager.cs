 using System;
 using System.Collections;
using System.Collections.Generic;
 using System.IO;
 using System.Linq;
 using JetBrains.Annotations;
 using Microsoft.Win32.SafeHandles;
 using UnityEditor.ShaderGraph.Drawing;
 using UnityEngine;
 using Random = UnityEngine.Random;

 public class AudioManager : MonoBehaviour
{
    // public static AudioManager instance;
    public List<AudioUnits> SFX;
    
    //checking to see if audio is playing
    public bool isPlaying;


    // private void Awake()
    // {
    //     // if (instance == null)
    //     // {
    //     //     instance = this; 
    //     //     
    //     //     DontDestroyOnLoad(gameObject);
    //     // }
    //     // else
    //     // {
    //     //     Destroy(gameObject);
    //     // }
    // }

    public void playSFX(int index)
    {
        float audDel;

        audDel = SFX[index].audioLength;
        
        // Debug.Log(Time.time);
        
        if (Time.time > audDel)
        {
            int randClip = Random.Range(0, SFX[index].audio.Length);
            AudioClip clipSele = SFX[index].audio[randClip];

            SFX[index].unitsSource.PlayOneShot(clipSele);
            isPlaying = true;

            SFX[index].audioLength = clipSele.length + Time.time;
        }
        
        
        // if (Time.time > clipSele.length + Time.time)
        // {
        //     isPlaying = false;
        // }
    }
    
    //extracts desired audio 
    public void LoadAudioClipsFromFolder(string folderName, int index)
    {
        string folderPath = Path.Combine("Assets/Resources", folderName);
        string[] audioFiles = Directory.GetFiles(folderPath, "*.mp3", SearchOption.AllDirectories);

        foreach (string audioFilePath in audioFiles)
        {
            string audioFileName = Path.GetFileNameWithoutExtension(audioFilePath);

            string audioPath = GetAudioResourcePath(audioFilePath); // Get the audio resource path
            
            AudioClip audioClip = Resources.Load<AudioClip>(audioPath);
            
            Debug.Log(audioPath);
            
            if (audioClip != null)
            {
                if (index < SFX.Count)
                {
                    // Initialize the audio array if it is null
                    if (SFX[index].audio == null)
                    {
                        SFX[index].audio = new AudioClip[0];
                    }
                    
                    SFX[index].audio = SFX[index].audio.Append(audioClip).ToArray();
                    SFX[index].AudioPath = audioPath;
                }
                else
                {
                    Debug.LogWarning("Index out of range: " + index);
                }
            }
            else
            {
                Debug.LogWarning("Failed to load audio clip: " + audioPath);
            }
        }
    }

    private string GetAudioResourcePath(string audioFilePath)
    {
        int startIndex = audioFilePath.IndexOf("Assets/Resources") + 17; // Update the start index
        int length = audioFilePath.LastIndexOf('.') - startIndex;
        string audioPath = audioFilePath.Substring(startIndex, length);
        return audioPath;
    }


}

 [Serializable]
 public class AudioUnits
 {
     public string AudioType;
     public GameObject unit;
     public AudioSource unitsSource;
     
     public string AudioPath { get; set; }
     public AudioClip[] audio;
     
     public float audioLength;
     
     /// <summary>
     /// constructor to add audio from folder within directory 
     /// </summary>
     /// <param name="audioPath"></param>
     /// <param name="audioClip"></param>
     public AudioUnits(string audioPath, AudioClip audioClip)
     {
         AudioPath = audioPath;
         audio = new AudioClip[] { audioClip };
     }

     // Constructor with audio clips and other options
     public AudioUnits(GameObject obj, AudioSource source)
     {
         unit = obj;
         unitsSource = source;
     }
     
     
 }