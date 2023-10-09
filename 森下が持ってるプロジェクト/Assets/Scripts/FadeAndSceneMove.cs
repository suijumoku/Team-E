using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeAndSceneMove : MonoBehaviour
{
    [Header("�t�F�[�h")] public FadeImage fade;
    [Header("�ړ�����V�[���̖��O")] public string NextSceneName;

    private bool StartedFade = false;
    private bool goNextScene = false;

    private void Start()
    {
        StartedFade = false;
        goNextScene = false;
    }
    //�X�^�[�g�{�^���������ꂽ��Ă΂��
    public void FadeStart()
    {
        Debug.Log("Start��������܂���");
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
