using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    SelectionScreen,
    CountDown,
    NormalEnemyBattle,
    BossEnemyBattle,
    Result
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private GameState currentGamestate;

    [Header("BGM用audiosource")][SerializeField] AudioSource BGM_AudioSource = null;
    [Header("SE用audiosource")][SerializeField] AudioSource SE_AudioSource = null;

    private void Awake()
    {
        //SetCurrentState(GameState.SelectionScreen);
        SetCurrentState(GameState.NormalEnemyBattle);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlayBGM(AudioClip bgm)
    {
        if (BGM_AudioSource != null)
        {
            Debug.Log("BGM_Set");
            BGM_AudioSource.clip = bgm;

            print("BGM_Play");
            BGM_AudioSource.Play();
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }
    public void PlayBGM(bool play)
    {
        if (BGM_AudioSource != null)
        {
            Debug.Log("オーディオソースが設定されていません");
            return;
        }
        if (play)
        {
            print("BGM_Play");
            BGM_AudioSource.Play();
        }
        else
        {
            print("BGM_Stop");
            BGM_AudioSource.Stop();
        }
    }
    public void PlaySE(AudioClip clip)
    {
        if (SE_AudioSource != null && clip != null)
        {
            SE_AudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }
    public void SetCurrentState(GameState State)
    {
        currentGamestate = State;
    }
    public GameState ReturnCurrentState()
    {
        return currentGamestate;
    }
}

