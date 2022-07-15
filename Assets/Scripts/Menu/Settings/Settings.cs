using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume") * 100;
    }

    public void SetVolume(float volume)
    {
        GameManager.Instance.SetVolume(volume);
    }
}
