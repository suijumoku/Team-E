using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    [Header("����(3��)")]
    [SerializeField] Image[] timeImg;

    [SerializeField] FadeAndSceneMove _fadeAndSceneMove;

    [SerializeField] int missCount = default!;
    [SerializeField] private BlinkingScript blinkingScript = default!;
    [SerializeField] ResultManager resultManager = default!;

    public bool isDefeat = false, isInvincible = false; //���G���Ԓ����ǂ���

    int[] timeArray;
    int currentCount = 0;
    float time = 0f, beforeTime, floarTime;
    GameObject obj = default;
    bool onlyF = false;
    //private MainGameManager instance;

    public void Awake()
    {
        //obj = GameObject.Find("ResultManager");
        //resultManager = obj.GetComponent<ResultManager>();  //resultmanager�̃A�^�b�`
    }

    void Start()
    {
        //SceneManager.activeSceneChanged += ActiveSceneChanged;
        //_PlayerController = GetComponent<PlayerController>();
        // _resultManager = _resultManager.GetComponent<ResultManager>();

        // Time.timeScale = 1.0f;
        onlyF = false;
        isDefeat = false;
        timeArray = new int[3] { 0, 0, 0 };
    }

    private void Update()
    {
        obj = GameObject.Find("ResultManager");
        resultManager = obj.GetComponent<ResultManager>();  //resultmanager�̍X�V

        if (UnityEngine.Input.GetKeyDown(KeyCode.P))    //�f�o�b�O�p�AP�Ń��U���g��
        {
            Defeat();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.L)) //L�Ń����[�h
        {
            SceneManager.LoadScene("Stage_syokyu");
        }

        if (ResultManager.isClear == true && onlyF == false)
        {
            Clear();
            onlyF = true;
            //ResultManager.isClear = false;
        }
        time += Time.deltaTime;

        floarTime = Mathf.Floor(time);   //�؂�̂�

        //Debug.Log("floarTime" + floarTime);

        if (floarTime - beforeTime >= 1.0f) //1�t���[���O�̎��Ԃ���ω����Ă�����(1�b�o�߂�����)�J��グ����
        {

            for (int i = 0; i < floarTime - beforeTime; i++)
            {
                timeArray[0]++;
                if (timeArray[0] >= 10)
                {
                    timeArray[1]++;        //�J��グ����
                    timeArray[0] = 0;
                }
                if (timeArray[1] >= 10)
                {
                    timeArray[2]++;
                    timeArray[1] = 0;
                }
            }
        }

        for (int i = 0; i < timeImg.Length; i++)
        {
            timeImg[i].sprite = resultManager.Numbers[timeArray[i]];

            timeImg[i].enabled = true;
        }


        if (isDefeat)
        {
           
            Defeat();
            isDefeat = false;
        }
        beforeTime = floarTime;

    }
    public void Miss()
    {
        if (currentCount >= missCount) return;

        InOrder(currentCount);
        //GameObject c = other.GetComponent<GameObject>();     

        currentCount++;
    }

    public void InOrder(int i)
    {
        blinkingScript.StartCoroutine(blinkingScript.DamageIndication(i));
    }

    public void Defeat()
    {
        Debug.Log("��������");
        _fadeAndSceneMove.FadeStart();
    }

    public void Clear()
    {
        Debug.Log("��������");
        obj = GameObject.Find("Life");
        blinkingScript = obj.GetComponent<BlinkingScript>();
        if (blinkingScript.life == 3)
        {
            resultManager.NoDmgBonus(); //���C�t���R�c���Ă���m�[�_���{�[�i�X
        }

        _fadeAndSceneMove.FadeStart();

    }

}
