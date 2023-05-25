using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioMixer audioMixer;
    public AudioClip[] musicArray;

    public Slider musicSlider;
    public Slider soundsSlider;

    public void Awake()
    {
        audioSourceMusic = GetComponent<AudioSource>();
        LoadSoundsOptions();
    }
    public void SetMusic()
    {
        audioSourceMusic.PlayOneShot(musicArray[Random.Range(0,musicArray.Length)]);
    }

    public void Update()
    {
        if (!audioSourceMusic.isPlaying) SetMusic();
    }

    public void SetVolumeSounds()
    {
        audioMixer.SetFloat("Sounds", Mathf.Log10(soundsSlider.value) *20);
        PlayerPrefs.SetFloat("Sounds", soundsSlider.value);
    }

    public void SetVolumeMusic()
    {
        audioMixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
    }

    private void LoadSoundsOptions()
    {
        if (PlayerPrefs.HasKey("Sounds") && PlayerPrefs.HasKey("Music"))
        {
            soundsSlider.value = PlayerPrefs.GetFloat("Sounds");
            musicSlider.value = PlayerPrefs.GetFloat("Music");
            SetVolumeSounds();
            SetVolumeMusic();
        }
    }
}
