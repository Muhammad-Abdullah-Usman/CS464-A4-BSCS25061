using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    void Start()
    {
        float saved = PlayerPrefs.GetFloat("MusicVolume", 1f);
        slider.minValue = 0.0001f;
        slider.maxValue = 1f;
        slider.value = saved;
        SetVolume(saved);
        slider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}