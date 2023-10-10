using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] AudioSource audioSource;

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
