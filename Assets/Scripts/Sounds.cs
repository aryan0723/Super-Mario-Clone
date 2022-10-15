using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]
public class Sounds 
{

    public string name;
    public AudioClip clip;

    [Range(0,5)]
    public float volume;


    [Range(0,3)]
    public float pitch;
    

   [HideInInspector]
   public AudioSource source ;
}
