using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [Header("シーン開始時のBGM")][SerializeField] AudioClip StartBGM;

    [Header("バトルBGM")][SerializeField] AudioClip NormalBattleBGM;
    [Header("自動遷移させる")][SerializeField] bool TransitionNormalBattleBGM = false;
    private bool DoNormalBattleBGMPlay = false;

    [Header("ボスバトルBGM")][SerializeField] AudioClip BossBattleBGM;
    [Header("自動遷移させる")][SerializeField] bool TransitionBossBattleBGM;
    private bool DoBossBattleBGMPlay = false;

    [Header("リザルトBGM")][SerializeField] AudioClip ResultBGM;
    [Header("自動遷移させる")][SerializeField] bool TransitionResultBGM;
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
