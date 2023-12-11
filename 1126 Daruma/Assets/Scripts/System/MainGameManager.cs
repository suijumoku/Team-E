using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    [Header("時間(3桁)")]
    [SerializeField] Image[] timeImg;

    [SerializeField] FadeAndSceneMove _fadeAndSceneMove;

    [SerializeField] int missCount = default!;
    [SerializeField] private BlinkingScript blinkingScript = default!;
    [SerializeField] ResultManager resultManager = default!;

    public bool isDefeat = false, isInvincible = false; //無敵時間中かどうか

    int[] timeArray;
    int currentCount = 0;
    float time = 0f, beforeTime, floarTime;
    GameObject obj = default;
    bool onlyF = false;
    //private MainGameManager instance;

    public void Awake()
    {
        //obj = GameObject.Find("ResultManager");
        //resultManager = obj.GetComponent<ResultManager>();  //resultmanagerのアタッチ
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
        resultManager = obj.GetComponent<ResultManager>();  //resultmanagerの更新

        if (UnityEngine.Input.GetKeyDown(KeyCode.P))    //デバッグ用、Pでリザルトへ
        {
            Defeat();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.L)) //Lでリロード
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

        floarTime = Mathf.Floor(time);   //切り捨て

        //Debug.Log("floarTime" + floarTime);

        if (floarTime - beforeTime >= 1.0f) //1フレーム前の時間から変化していたら(1秒経過したら)繰り上げ処理
        {

            for (int i = 0; i < floarTime - beforeTime; i++)
            {
                timeArray[0]++;
                if (timeArray[0] >= 10)
                {
                    timeArray[1]++;        //繰り上げ処理
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
        Debug.Log("負け判定");
        _fadeAndSceneMove.FadeStart();
    }

    public void Clear()
    {
        Debug.Log("勝ち判定");
        obj = GameObject.Find("Life");
        blinkingScript = obj.GetComponent<BlinkingScript>();
        if (blinkingScript.life == 3)
        {
            resultManager.NoDmgBonus(); //ライフが３残ってたらノーダメボーナス
        }

        _fadeAndSceneMove.FadeStart();

    }

}
