using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeAndSceneMove : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;
    [Header("移動するシーンの名前")] public string NextSceneName;

    private bool StartedFade = false;
    private bool goNextScene = false;

    private void Start()
    {
        StartedFade = false;
        goNextScene = false;
    }
    //スタートボタンを押されたら呼ばれる
    public void FadeStart()
    {
        Debug.Log("Startが押されました");
        if (!StartedFade)
        {
            fade.StartFadeOut();
            StartedFade = true;
        }
    }
    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene(NextSceneName);
            goNextScene = true;
        }
        StartedFade = false;
        goNextScene = false;
    }
}
