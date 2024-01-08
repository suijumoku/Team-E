using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] AudioSource audioSource;
    [SerializeField] bool GetBGM_AudioSource;
    [SerializeField] bool GetSE_AudioSource;

    private void Awake()
    {
        if(GetBGM_AudioSource)
        audioSource = GameManager.instance.BGM_AudioSource;
        else if(GetSE_AudioSource)
        audioSource = GameManager.instance.SE_AudioSource;
    }

    public void ChangeFillAmount(float SliderValue)
    {
        Image.fillAmount = SliderValue;
        print(SliderValue);
    }
    public void ChangeValume(float SliderValue)
    {
        audioSource.volume = SliderValue;
        print(SliderValue);
    }

}
