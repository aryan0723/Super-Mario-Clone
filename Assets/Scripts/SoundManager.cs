using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sounds[] sounds;
    void Awake(){
        foreach(Sounds s in sounds){
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip=s.clip;
            s.source.volume=s.volume;
            s.source.pitch=s.pitch;
        }
    }

    void  Start()
    {
        Play("Theme");
    }

    public void Play( string name){
        Sounds s=Array.Find(sounds ,sound=>sound.name==name);
        s.source.Play();
    }
    public Sounds Sounditem (string name)
    {
        for (int i = 0; i < sounds.Length; i++) 
        {
            if (sounds[i].name == name)
            return sounds[i];
        }
 
        Debug.Log ("No item has the name '" + name + "'.");
        return null;
    }
}
