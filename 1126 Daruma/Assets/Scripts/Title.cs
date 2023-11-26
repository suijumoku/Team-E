using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    [Header("フェード")] public FadeAndSceneMove fade;

    private bool firstPush = false;

    private void Start()
    {
        firstPush = false;
    }
    //スタートボタンを押されたら呼ばれる
    public void StartGame()
    {
        Debug.Log("Startが押されました");
        if (!firstPush)
        {
            fade.FadeStart();
            firstPush = true;
        }
    }
    public void QuitGame()
    {
        print("Quitが押されました");

        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
             Application.Quit();
        #endif    
    }
}