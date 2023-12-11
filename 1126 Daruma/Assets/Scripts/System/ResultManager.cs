using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [Header("�X�R�A")]
    [SerializeField] public Image[] scoreImg;  //[i][j] i:�e���q j:�\�̈ʂ���̈�
    [Header("�{�X�X�R�A")]
    [SerializeField] Image[] bossScoreImg;
    [Header("�q����܃X�R�A")]
    [SerializeField] Image[] kidScoreImg;
    [Header("����")]
    [SerializeField] Image noDmgImg;

    [Header("�X�R�A�̐���")]
    [SerializeField] public Sprite[] Numbers;
    [Header("���U���g�摜����")]
    [SerializeField] Image[] results;
    [Header("�\���T�E���h")]
    [Tooltip("0���h���I�A1���h�h���I��")]
    [SerializeField] AudioClip[] indicateS = default!;
    [Header("���U���g��")]
    [Tooltip("0��s�k���A�P����������")]
    [SerializeField] AudioClip[] resultSounds = default!;

    [SerializeField] ScoreUI _ScoreUI;

    //[Header("�L���ɂȂ�������s����")]
    //[SerializeField] bool OnEneble;
    [Header("�V�[�����ǂݍ��܂ꂽ����s����")]
    [SerializeField] bool OnLoadScene;
    AudioClip resultS = default!; 

    [SerializeField] int hit = 1, doubleHit = 2, beatBoss = 4, noDamage = 20, Bonus_Standard_Time = 60, timeBonus = 20, tourou = 2;
    [SerializeField] float duration = 0.5f;

    //const int score = 0, boss = 1, kid = 2;
    const int tensPlace = 0, onePlace = 1;
    [SerializeField] int clearValue = 5;  //���U�܌j��ŃN���A

    int[] scoreArray, Boss_Time_Array, kidArray;
    public static int calcScore = 0, beatDarumaValue = 0, breakTourouValue = 0;
    bool isResult = true, isNoDmg = false;
    public bool isClear = false;


    //private ResultManager instance;


    [SerializeField] int num = default!; //�f�o�b�O�p
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
    }

    void Start()
    {
        if (OnLoadScene)
        {
            Debug.Log("OnLoadScene");
            Time.timeScale = 1;
            StartCoroutine(ResultCorutine());
        }
        calcScore = 0; beatDarumaValue = 0; breakTourouValue = 0;
    }

    void Update()
    {
     
    } 

    public void NormalHit()
    {
        //���ʂɔ�΂���+1
        Debug.Log("hit = " + hit);
        calcScore += hit;
       _ScoreUI.ScoreUpdate();
    }

    public void DoubleHit()
    {
        //���Ăď�����+2
        calcScore += doubleHit;
       _ScoreUI.ScoreUpdate();
    }
    public void BeatDaruma()
    {
        beatDarumaValue++;      
        
     // _ScoreUI.ScoreUpdate();
    }
    public void BeatBoss(float time)
    {
        //�{�X�|����+4
        calcScore += beatBoss;
        if (time <= Bonus_Standard_Time)    //�{�X��|�������_�ŃN���A�^�C��(�o�����Ă���|���܂ŁH)��60�b������Ă�����{�[�i�X�_
        {
            calcScore += timeBonus;
        }

        for (int i = 0; i < time; i++)  //����time��float���Ƃ��܂������Ȃ����� -> �J��グ��ꂿ�Ⴄ�A�v���P
        {
            Boss_Time_Array[0]++;
            if (Boss_Time_Array[0] >= 10)
            {
                Boss_Time_Array[1]++;
                Boss_Time_Array[0] = 0;
            }
        }
         _ScoreUI.ScoreUpdate();
    }

    public void NoDmgBonus() //����PlayerController�ŌĂ�->MainGameManager
    {
        calcScore += noDamage;
        isNoDmg = true;
    }

    public void breakTourou()
    {
        //���U�j���+2�_
        calcScore += tourou;
        breakTourouValue++;
        if (breakTourouValue >= clearValue)
        {
            isClear = true;
        }
        _ScoreUI.ScoreUpdate();
    }

    public void Assign(bool isResult)
    {
        scoreArray[0] = 0;
        scoreArray[1] = 0;
        for (int i = 0; i < calcScore; i++)
        {
            scoreArray[0]++;
            if (scoreArray[0] >= 10)
            {
                scoreArray[1]++;        //�X�R�A���񌅂Ƃ����O��ŌJ��グ����
                scoreArray[0] = 0;
            }
        }

        //�X�R�A�ɑΉ����������̉摜������
        scoreImg[tensPlace].sprite = Numbers[scoreArray[1]];
        scoreImg[onePlace].sprite = Numbers[scoreArray[0]];

        //Debug.Log("scoreArray[0] = " + scoreArray[0]);
        //Debug.Log("scoreArray[1] = " + scoreArray[1]);
        //Debug.Log("Numbers[scoreArray[1]] = " + Numbers[scoreArray[1]]);
        //Debug.Log(" scoreImg[tensPlace].sprite = " + scoreImg[tensPlace].sprite);
        //Debug.Log(" scoreImg[onePlace].sprite = " + scoreImg[onePlace].sprite);

        if (isResult == true)   //���U���g��ʂȂ�{�X�̕b���Ǝq����܂̐��������
        {
            //bossScoreImg[tensPlace].sprite = Numbers[Boss_Time_Array[1]]; //Numbers[i] i:���X�N���v�g����擾�����X�R�A���\�̈ʂƈ�̈ʂɕ������ē����
            //bossScoreImg[onePlace].sprite = Numbers[Boss_Time_Array[0]];
            for (int i = 0; i < beatDarumaValue; i++)
            {
                kidArray[0]++;
                if (kidArray[0] >= 10)
                {
                    kidArray[1]++;
                    kidArray[0] = 0;
                }
            }
          

            kidScoreImg[tensPlace].sprite = Numbers[kidArray[1]];
            kidScoreImg[onePlace].sprite = Numbers[kidArray[0]];
        }
        Debug.Log("calcScore = " + calcScore);

    }
    private IEnumerator ResultCorutine()
    {

        //NormalHit();
        //DoubleHit();
        //BeatBoss(10.2f);
        //BeatDaruma();   //1+3+4+20+16, 10�b�A01�� �ǔ���
        //calcScore += noDamage;
        //calcScore = num;
        //isNoDmg = true;
        //�f�o�b�O�p��

        Assign(isResult);
        yield return new WaitForSeconds(duration);  //�n�܂�Ɠ����ɕ\�������̂�h�����߂ɏ����Ԃ��󂯂�

        IndicateScore(scoreImg, indicateS[0]);
        yield return new WaitForSeconds(duration);

        IndicateScore(bossScoreImg, indicateS[0]);
        yield return new WaitForSeconds(duration);

        IndicateScore(kidScoreImg, indicateS[0]);

        if (isNoDmg == true)        //�m�[�_����������`�F�b�N�����
        {
            yield return new WaitForSeconds(duration);
            GameManager.instance.PlaySE(indicateS[0]);
            noDmgImg.enabled = true;
        }
        yield return new WaitForSeconds(duration * 1.5f);
        GameManager.instance.PlaySE(indicateS[1]);
        // result.enabled = true;       

     

        if (isClear == false)  //�v���� �������₷��������@�킩��Ȃ�
        {
            results[0].enabled = true; //����
            resultS = resultSounds[0];
        }
        else if (isClear == true && calcScore < 20)
        {
            results[1].enabled = true; //��
            resultS = resultSounds[1];
        }
        else if (calcScore >= 20 && calcScore < 30)
        {
            results[2].enabled = true; //��
            resultS = resultSounds[1];
        }
        else if (calcScore >= 30 && calcScore < 50)
        {
            results[3].enabled = true; //�D
            resultS = resultSounds[1];
        }
        else if (calcScore >= 50)
        {
            results[4].enabled = true; //�G
            resultS = resultSounds[1];
        }

        yield return new WaitForSeconds(duration);
        GameManager.instance.PlaySE(resultS);

        //Debug.Log("score.enabled0 = " + scoreImg[0].enabled);
        //Debug.Log("score.enabled1 = " + scoreImg[1].enabled);

        //Debug.Log("bossScore.enabled0 = " + bossScoreImg[0].enabled);
        //Debug.Log("bossScore.enabled1 = " + bossScoreImg[1].enabled);

        //Debug.Log("kidScore.enabled0 = " + kidScoreImg[0].enabled);
        //Debug.Log("kidScore.enabled1 = " + kidScoreImg[1].enabled);
        yield return null;
    }

    private void IndicateScore(Image[] Img, AudioClip clip)
    {
        //�T�E���h
        GameManager.instance.PlaySE(clip);
        Img[tensPlace].enabled = true;
        Img[onePlace].enabled = true;

        
        //�኱�̒x��
    }
}
