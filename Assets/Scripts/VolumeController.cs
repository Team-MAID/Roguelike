using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    { 
        mixer.SetFloat("GameMusic", Mathf.Log(1) * 10f);
        mixer.SetFloat("GameSound", Mathf.Log(1) * 10f);
        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateMusicValue(float value)
    {
        mixer.SetFloat("GameMusic", Mathf.Log(value) * 10f);
    }
    public void UpdateSoundValue(float value)
    {
        mixer.SetFloat("GameSound", Mathf.Log(value) * 10f);
    }
}
