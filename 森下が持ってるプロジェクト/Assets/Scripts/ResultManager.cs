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
    private Image result;


    const int boss = 0, kid = 1;
    const int tensPlace = 0, onePlace = 1;


    void Start()
    {
        //GetScore�������Ȋ֐����΂悳����

        score[boss][tensPlace].sprite = Numbers[0]; //Numbers[i] i:���X�N���v�g����擾�����X�R�A���\�̈ʂƈ�̈ʂɕ������ē����
        score[boss][onePlace].sprite = Numbers[0];

        score[kid][tensPlace].sprite = Numbers[0];
        score[kid][onePlace].sprite = Numbers[0];

        result.sprite = results[1]; //results[i] i�̒l��ς���

        for (int i = 0; i < 2; i++)
        {
            //�T�E���h
            for (int j = 0; j < 2; j++)
            {
                score[i][j].enabled = true;
            }
            //�኱�̒x��
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
