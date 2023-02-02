using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sound_slider : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
        Debug.Log(volume);

    }
    
}
