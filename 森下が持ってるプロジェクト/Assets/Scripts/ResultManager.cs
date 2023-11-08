using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //[Header("スコア")]
    [SerializeField] Image[][] scoreImg;  //[i][j] i:親か子 j:十の位か一の位
    [Header("スコアの数字")]
    [SerializeField] Sprite[] Numbers;
    [Header("リザルト画像たち")]
    [SerializeField] Sprite[] results;
    [Header("表示サウンド")]
    [Tooltip("0をドン！、1をドドン！に")]
    [SerializeField] AudioClip[] indicateS = default!;
    [Header("リザルト音")]
    [Tooltip("0を敗北音、１を勝利音に")]
    [SerializeField] AudioClip[] resultSounds = default!;

    //[Header("有効になったら実行する")]
    //[SerializeField] bool OnEneble;
    [Header("シーンが読み込まれたら実行する")]
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
        //最初に全てのLife画像をtrueに
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
        //普通に飛ばすと+1
        calcScore = hit;
    }

    public void DoubleHit()
    {
        //当てて消すと+3
        calcScore += doubleHit;
    }

    public void BeatBoss(float time)
    {
        //ボス倒すと+4
        calcScore += beatBoss;
        if (time <= Bonus_Standard_Time)    //ボスを倒した時点でクリアタイム(出現してから倒すまで？)が60秒下回っていたらボーナス点
        {
            calcScore += timeBonus;
        }

        for (int i = 0; i < time; i++)  //これtimeがfloatだとうまく動かないかも
        {
            Boss_Time_Array[0]++;
            if (Boss_Time_Array[0] >= 10)
            {
                Boss_Time_Array[1]++;
                Boss_Time_Array[0] = 0;
            }
        }
           
    }

    public void NoDmgBonus() //多分PlayerControllerで呼ぶ
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
                scoreArray[1]++;        //スコアが二桁という前提で繰り上げ処理
                scoreArray[0] = 0;
            }
        }

        //スコアに対応した数字の画像を表示
        scoreImg[score][tensPlace].sprite = Numbers[scoreArray[1]];
        scoreImg[score][onePlace].sprite = Numbers[scoreArray[0]];

        scoreImg[boss][tensPlace].sprite = Numbers[Boss_Time_Array[1]]; //Numbers[i] i:他スクリプトから取得したスコアを十の位と一の位に分割して入れる
        scoreImg[boss][onePlace].sprite = Numbers[Boss_Time_Array[0]];

        scoreImg[kid][tensPlace].sprite = Numbers[kidArray[1]];
        scoreImg[kid][onePlace].sprite = Numbers[kidArray[0]];

        if (calcScore < 4)  //要調整 調整しやすくする方法わからない
        {
            result.sprite = results[0]; //負け
            resultS = resultSounds[0];
        }
        else if (calcScore >= 4 && calcScore < 20)
        {
            result.sprite = results[1]; //可
            resultS = resultSounds[1];
        }
        else if (calcScore >= 20 && calcScore < 40)
        {
            result.sprite = results[2]; //良
            resultS = resultSounds[1];
        }
        else if (calcScore >= 40 && calcScore < 80)
        {
            result.sprite = results[3]; //優
            resultS = resultSounds[1];
        }
        else if (calcScore >= 80)
        {
            result.sprite = results[4]; //秀
            resultS = resultSounds[1];
        }

    }
    public void IndicateScore()
    {
        Assign();

        for (int i = 0; i < 3; i++) 
        {
            //サウンド
            GameManager.instance.PlaySE(indicateS[0]);
            scoreImg[i][tensPlace].enabled = true;
            scoreImg[i][onePlace].enabled = true;
            new WaitForSeconds(duration);
            //若干の遅延
        }
        new WaitForSeconds(duration);
        GameManager.instance.PlaySE(indicateS[1]);
        result.enabled = true;

        new WaitForSeconds(duration);
        GameManager.instance.PlaySE(resultS);
       
    }
}
