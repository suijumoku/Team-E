using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //[Header("�X�R�A")]
    [SerializeField] Image[][] scoreImg;  //[i][j] i:�e���q j:�\�̈ʂ���̈�
    [Header("�X�R�A�̐���")]
    [SerializeField] Sprite[] Numbers;
    [Header("���U���g�摜����")]
    [SerializeField] Sprite[] results;
    [Header("�\���T�E���h")]
    [Tooltip("0���h���I�A1���h�h���I��")]
    [SerializeField] AudioClip[] indicateS = default!;
    [Header("���U���g��")]
    [Tooltip("0��s�k���A�P����������")]
    [SerializeField] AudioClip[] resultSounds = default!;

    //[Header("�L���ɂȂ�������s����")]
    //[SerializeField] bool OnEneble;
    [Header("�V�[�����ǂݍ��܂ꂽ����s����")]
    [SerializeField] bool OnLoadScene;

    AudioClip resultS = default!;
    //[SerializeField] AudioClip[] BGM = default!;

    [SerializeField] int hit = 1, doubleHit = 3, beatBoss = 4, noDamage = 16, Bonus_Standard_Time = 60, timeBonus = 20;
    private Image result;

    const int  score = 0,boss = 1, kid = 2;
    const int tensPlace = 0, onePlace = 1;
    const float duration = 0.5f;
    int[] scoreArray, Boss_Time_Array, kidArray;
  

    int calcScore = 0;

    //void OnEnable()
    //{
    //    if (OnEneble)
    //    {
    //        Debug.Log("OnEnable");

    //    }
    //}

    void Awake()
    {
        scoreArray = new int[2] { 0, 0 };
        Boss_Time_Array = new int[2] { 0, 0 };
        kidArray = new int[2] { 0, 0 };
        //�ŏ��ɑS�Ă�Life�摜��true��
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                scoreImg[i][j].sprite = Numbers[0];
            }              
        }
    }

    void Start()
    {
        if (OnLoadScene)
        {
            Debug.Log("OnLoadScene");
            IndicateScore();
        }
    }

    void Update()
    {
      
    }
   

    public void beatDaruma()
    {
        kidArray[0]++;
        if (kidArray[0] >= 10)
        {
            kidArray[1]++;
            kidArray[0] = 0;
        }
    }

    public void NormalHit()
    {
        //���ʂɔ�΂���+1
        calcScore = hit;
    }

    public void DoubleHit()
    {
        //���Ăď�����+3
        calcScore += doubleHit;
    }

    public void BeatBoss(float time)
    {
        //�{�X�|����+4
        calcScore += beatBoss;
        if (time <= Bonus_Standard_Time)    //�{�X��|�������_�ŃN���A�^�C��(�o�����Ă���|���܂ŁH)��60�b������Ă�����{�[�i�X�_
        {
            calcScore += timeBonus;
        }

        for (int i = 0; i < time; i++)  //����time��float���Ƃ��܂������Ȃ�����
        {
            Boss_Time_Array[0]++;
            if (Boss_Time_Array[0] >= 10)
            {
                Boss_Time_Array[1]++;
                Boss_Time_Array[0] = 0;
            }
        }
           
    }

    public void NoDmgBonus() //����PlayerController�ŌĂ�
    {
        calcScore += noDamage;
    }   

    void Assign()
    {
        for (int i = 0; i < calcScore; i++)
        {
            scoreArray[0]++;
            if (scoreArray[0] >= 10)
            {
                scoreArray[1]++;        //�X�R�A���񌅂Ƃ����O��ŌJ��グ����
                scoreArray[0] = 0;
            }
        }

        //�X�R�A�ɑΉ����������̉摜��\��
        scoreImg[score][tensPlace].sprite = Numbers[scoreArray[1]];
        scoreImg[score][onePlace].sprite = Numbers[scoreArray[0]];

        scoreImg[boss][tensPlace].sprite = Numbers[Boss_Time_Array[1]]; //Numbers[i] i:���X�N���v�g����擾�����X�R�A���\�̈ʂƈ�̈ʂɕ������ē����
        scoreImg[boss][onePlace].sprite = Numbers[Boss_Time_Array[0]];

        scoreImg[kid][tensPlace].sprite = Numbers[kidArray[1]];
        scoreImg[kid][onePlace].sprite = Numbers[kidArray[0]];

        if (calcScore < 4)  //�v���� �������₷��������@�킩��Ȃ�
        {
            result.sprite = results[0]; //����
            resultS = resultSounds[0];
        }
        else if (calcScore >= 4 && calcScore < 20)
        {
            result.sprite = results[1]; //��
            resultS = resultSounds[1];
        }
        else if (calcScore >= 20 && calcScore < 40)
        {
            result.sprite = results[2]; //��
            resultS = resultSounds[1];
        }
        else if (calcScore >= 40 && calcScore < 80)
        {
            result.sprite = results[3]; //�D
            resultS = resultSounds[1];
        }
        else if (calcScore >= 80)
        {
            result.sprite = results[4]; //�G
            resultS = resultSounds[1];
        }

    }
    public void IndicateScore()
    {
        Assign();

        for (int i = 0; i < 3; i++) 
        {
            //�T�E���h
            GameManager.instance.PlaySE(indicateS[0]);
            scoreImg[i][tensPlace].enabled = true;
            scoreImg[i][onePlace].enabled = true;
            new WaitForSeconds(duration);
            //�኱�̒x��
        }
        new WaitForSeconds(duration);
        GameManager.instance.PlaySE(indicateS[1]);
        result.enabled = true;

        new WaitForSeconds(duration);
        GameManager.instance.PlaySE(resultS);
       
    }
}
