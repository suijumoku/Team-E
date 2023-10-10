using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("シーン開始時のBGM")][SerializeField]AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.PlayBGM(bgm);
    }
}
