using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [Header("シーン開始時のBGM")][SerializeField]AudioClip StartBGM;
    [Header("バトルBGM")][SerializeField]AudioClip BattleBGM;
    [Header("ボスバトルBGM")][SerializeField]AudioClip BossBattleBGM;
    [Header("リザルトBGM")][SerializeField]AudioClip ResultBGM;

    void Start()
    {
        GameManager.instance.PlayBGM(StartBGM);
    }

    void BattleBGMPlay()
    {
        GameManager.instance.PlayBGM(BattleBGM);
    }

    void BossBattleBGMPlay()
    {
        GameManager.instance.PlayBGM(BossBattleBGM);
    }

    void ResultBGMPlay()
    {
        GameManager.instance.PlayBGM(ResultBGM);
    }
}
