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

    /// <summary>
    /// When the music switch in the settings menu is changed, the volume field in each audio labelled as game music is also changed
    /// </summary>
    public void UpdateMusicValue(float value)
    {
        mixer.SetFloat("GameMusic", Mathf.Log(value) * 10f);
    }
    /// <summary>
    /// When the audio switch in the settings menu is changed, the volume field in each audio labelled as game sound is also changed
    /// </summary>
    public void UpdateSoundValue(float value)
    {
        mixer.SetFloat("GameSound", Mathf.Log(value) * 10f);
    }
}
