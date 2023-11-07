using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [Header("�V�[���J�n����BGM")][SerializeField] AudioClip StartBGM;

    [Header("�o�g��BGM")][SerializeField] AudioClip NormalBattleBGM;
    [Header("�����J�ڂ�����")][SerializeField] bool TransitionNormalBattleBGM = false;
    private bool DoNormalBattleBGMPlay = false;

    [Header("�{�X�o�g��BGM")][SerializeField] AudioClip BossBattleBGM;
    [Header("�����J�ڂ�����")][SerializeField] bool TransitionBossBattleBGM;
    private bool DoBossBattleBGMPlay = false;

    [Header("���U���gBGM")][SerializeField] AudioClip ResultBGM;
    [Header("�����J�ڂ�����")][SerializeField] bool TransitionResultBGM;
    private bool DoResultBGMPlay = false;


    void Start()
    {
        GameManager.instance.PlayBGM(StartBGM);
    }
    private void Update()
    {
        if (GameManager.instance.ReturnCurrentState() == GameState.NormalEnemyBattle && TransitionNormalBattleBGM&&!DoNormalBattleBGMPlay)
        {
            NormalBattleBGMPlay();
            DoNormalBattleBGMPlay=true;
        }
        if (GameManager.instance.ReturnCurrentState() == GameState.BossEnemyBattle && TransitionBossBattleBGM&&!DoBossBattleBGMPlay)
        {
            BossBattleBGMPlay();
            DoBossBattleBGMPlay=true;
        }
        if (GameManager.instance.ReturnCurrentState() == GameState.Result && TransitionResultBGM&&!DoResultBGMPlay)
        {
            ResultBGMPlay();
            DoResultBGMPlay=true;
        }
    }

    void NormalBattleBGMPlay()
    {
        GameManager.instance.PlayBGM(NormalBattleBGM);
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
