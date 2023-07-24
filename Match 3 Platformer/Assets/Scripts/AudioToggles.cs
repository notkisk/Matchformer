using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioToggles : MonoBehaviour
{
    private Toggle _toggle;

    public bool isSFX, isMusic;
  
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();

        if (isSFX)
        {
            if (PlayerPrefs.GetFloat("SfxVolume", 0f) == 0f)
            {
                _toggle.isOn = false;
            }
            else
            {
                _toggle.isOn = true;

            }
        }
        else if (isMusic)
        {
            if (PlayerPrefs.GetFloat("MusicVolume", 0f) == 0f)
            {
                _toggle.isOn = false;

            }
            else
            {
                _toggle.isOn = true;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_toggle.isOn)
        {
            if (isSFX)
            {
                FindObjectOfType<AudioManager>().SetVolume("SfxVolume", -80f);
            }
            else if (isMusic)
            {
                FindObjectOfType<AudioManager>().SetVolume("MusicVolume", -80f);

            }
        }
        if (!_toggle.isOn)
        {
            if (isSFX)
            {
                FindObjectOfType<AudioManager>().SetVolume("SfxVolume", 0f);
            }
            else if (isMusic)
            {
                FindObjectOfType<AudioManager>().SetVolume("MusicVolume", 0f);

            }
        }
        if (isSFX)
        {
            if (PlayerPrefs.GetFloat("SfxVolume", 0f) == 0f)
            {
                _toggle.isOn = false;
            }
            else
            {
                _toggle.isOn = true;

            }
        }
        else if (isMusic)
        {
            if (PlayerPrefs.GetFloat("MusicVolume", 0f) == 0f)
            {
                _toggle.isOn = false;

            }
            else
            {
                _toggle.isOn = true;

            }
        }
     

    }
}
