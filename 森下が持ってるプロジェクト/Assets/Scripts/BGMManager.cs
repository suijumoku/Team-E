using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [Header("�V�[���J�n����BGM")][SerializeField]AudioClip StartBGM;
    [Header("�o�g��BGM")][SerializeField]AudioClip BattleBGM;
    [Header("�{�X�o�g��BGM")][SerializeField]AudioClip BossBattleBGM;
    [Header("���U���gBGM")][SerializeField]AudioClip ResultBGM;

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
