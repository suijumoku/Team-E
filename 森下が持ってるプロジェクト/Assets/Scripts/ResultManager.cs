using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //[Header("�X�R�A")]
    [SerializeField] Image[][] score;  //[i][j] i:�e���q j:�\�̈ʂ���̈�
    [Header("�X�R�A�̐���")]
    [SerializeField] Sprite[] Numbers;
    [Header("���U���g�摜����")]
    [SerializeField] Sprite[] results;
    [Header("�\�����̃T�E���h")]
    [SerializeField] AudioClip[] indicateS = default!;
    [Header("�\����")]
    [SerializeField] AudioClip clapS = default!;

    [SerializeField] AudioClip BGM = default!;
    private Image result;

    const int boss = 0, kid = 1;
    const int tensPlace = 0, onePlace = 1;
    const float duration = 0.5f;


    void Start()
    {
        //GetScore�������Ȋ֐����΂悳����

        score[boss][tensPlace].sprite = Numbers[0]; //Numbers[i] i:���X�N���v�g����擾�����X�R�A���\�̈ʂƈ�̈ʂɕ������ē����
        score[boss][onePlace].sprite = Numbers[0];

        score[kid][tensPlace].sprite = Numbers[0];
        score[kid][onePlace].sprite = Numbers[0];

        result.sprite = results[1]; //results[i] i�̒l��ς���
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Indicate()
    {

        for (int i = 0; i < 2; i++)
        {
            //�T�E���h
            GameManager.instance.PlaySE(indicateS[1]);
            score[i][tensPlace].enabled = true;
            score[i][onePlace].enabled = true;
            new WaitForSeconds(duration);
            //�኱�̒x��
        }

        GameManager.instance.PlaySE(indicateS[2]);
        result.enabled = true;
    }
}
