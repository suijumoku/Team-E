using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    [Header("�t�F�[�h")] public Fade fade;

    private bool firstPush = false;

    private void Start()
    {
        firstPush = false;
    }
    //�X�^�[�g�{�^���������ꂽ��Ă΂��
    public void StartGame()
    {
        Debug.Log("Start��������܂���");
        if (!firstPush)
        {
            fade.FadeStart();
            firstPush = true;
        }
    }
    public void QuitGame()
    {
        print("Quit��������܂���");
        Application.Quit();
    }
}