using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
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
            StartCoroutine(LoadNextSceneAsync());

        }
    }
    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            print(NextSceneName);
            goNextScene = true;
        }
    }
    private IEnumerator LoadNextSceneAsync()
    {
        // �t�F�[�h�A�E�g���I����Ă���V�[���ړ�������
        while (!goNextScene)
        {
            yield return null;
        }
        AsyncOperation async = SceneManager.LoadSceneAsync(NextSceneName);

        async.allowSceneActivation = false; 



        async.allowSceneActivation = true;

    }
}
