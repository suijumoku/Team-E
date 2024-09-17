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
}public enum InputState
{
    None,
    OnInput,
    OffInput
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private GameState currentGamestate;
    private InputState currentInputstate;

    [Header("BGM用audiosource")][SerializeField] public AudioSource BGM_AudioSource = null;
    [Header("SE用audiosource")][SerializeField] public AudioSource SE_AudioSource = null;

    private void Awake()
    {
        SetCurrentState(GameState.SelectionScreen);
        InputStateOn();
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
        if (BGM_AudioSource == null)
        {
            Debug.Log("オーディオソースが設定されていません");
        }
        else
        {
            Debug.Log("BGM_Set");
            BGM_AudioSource.clip = bgm;

            print("BGM_Play");
            BGM_AudioSource.Play();
        }
    }
    public void PlayBGM(bool play)
    {
        if (BGM_AudioSource == null)
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


    public void InputStateOn()
    {
        currentInputstate = InputState.OnInput;
    } 
    public void InputStateOff()
    {
        currentInputstate = InputState.OffInput;
    }

    public InputState ReturnInputState()
    {
        return currentInputstate;
    }
}

