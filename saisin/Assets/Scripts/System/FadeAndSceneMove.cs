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
            StartCoroutine(LoadScene());
            StartedFade = true;

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
    private IEnumerator LoadScene()
    {
        var async = SceneManager.LoadSceneAsync(NextSceneName);

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(1.5f);
        async.allowSceneActivation = true;
    }
}
