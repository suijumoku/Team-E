using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [Header("今のシーンの名前(再挑戦用)")]
    [SerializeField] public string InputSceneName;

    [Header("スコア")]
    [SerializeField] public Image[] scoreImg;  //[i][j] i:親か子 j:十の位か一の位
    [Header("ボススコア")]
    [SerializeField] Image[] bossScoreImg;
    [Header("子だるまスコア")]
    [SerializeField] Image[] kidScoreImg;
    [Header("無傷")]
    [SerializeField] Image noDmgImg;
    [Header("スコアの数字")]
    [SerializeField] public Sprite[] Numbers;
    [Header("リザルト画像たち")]
    [SerializeField] Image[] results;
    [Header("表示サウンド")]
    [Tooltip("0をドン！、1をドドン！に")]
    [SerializeField] AudioClip[] indicateS = default!;
    [Header("リザルト音")]
    [Tooltip("0を敗北音、１を勝利音に")]
    [SerializeField] AudioClip[] resultSounds = default!;

    [SerializeField] ScoreUI _ScoreUI;
    [SerializeField] FadeAndSceneMove _FadeAndSceneMove;

    //[Header("有効になったら実行する")]
    //[SerializeField] bool OnEneble;
    [Header("シーンが読み込まれたら実行する")]
    [SerializeField] bool OnLoadScene;
    AudioClip resultS = default!; 

    [SerializeField] int hit = 1, doubleHit = 2, /*beatBoss = 4,*/ noDamage = 20, Bonus_Standard_Time = 60, timeBonus = 5, tourou = 10;
    [SerializeField] float duration = 0.5f;

    //const int score = 0, boss = 1, kid = 2;
    const int tensPlace = 0, onePlace = 1, maxScore = 99;
    [SerializeField] int clearValue = 5;  //灯篭五個破壊でクリア

    int[] scoreArray, Boss_Time_Array, kidArray;
    public static int calcScore = 0, beatDarumaValue = 0, breakTourouValue = 0;
    bool isResult = true;
    public static bool isClear = false;
    public static bool isNoDmg = false;

    private bool clearState = false;
    private bool noDmgState = false;
    public static string SceneName;
    [SerializeField]
    private int indicateScore = 0;
    private int indicateDaruma = 0;
   // private int indicateTourou = 0; //コルーチンの最後に初期化すると最速でボタン押してシーン移動した時にバグるので別の変数に移す
    

  //  [SerializeField] int num = default!; //デバッグ用


    void Awake()
    {
        scoreArray = new int[2] { 0, 0 };
        Boss_Time_Array = new int[2] { 0, 0 };
        kidArray = new int[2] { 0, 0 };
        if (OnLoadScene)
        {
            indicateScore = 0;
            Debug.Log("OnLoadScene");
            Time.timeScale = 1;
            StartCoroutine(ResultCorutine());
        }
        SceneName = InputSceneName;
    }

    void Start()
    {
//staticのobjectに名前を入れることでResultSceneに名前を引き継いで再挑戦ができるように
    }

    public void NormalHit()
    {
        //普通に飛ばすと+1
        Debug.Log("hit = " + hit);
        calcScore += hit;
       _ScoreUI.ScoreUpdate();
    }

    public void DoubleHit()
    {
        //当てて消すと加点
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
        //ボス倒すと加点
        //calcScore += beatBoss;
        if (time <= Bonus_Standard_Time)    //ボスを倒した時点でクリアタイム(出現してから倒すまで？)が60秒下回っていたらボーナス点
        {
            calcScore += timeBonus;
        }

        for (int i = 0; i < time; i++)  //これtimeがfloatだとうまく動かないかも -> 繰り上げられちゃう、要改善
        {
            Boss_Time_Array[0]++;
            if (Boss_Time_Array[0] >= 10)
            {
                Boss_Time_Array[1]++;
                Boss_Time_Array[0] = 0;
            }
        }
         //_ScoreUI.ScoreUpdate();
    }

    public void NoDmgBonus() //多分PlayerControllerで呼ぶ->MainGameManager
    {        
        calcScore += noDamage;
        isNoDmg = true;
        Debug.Log("isNoDmg1 = " + isNoDmg);
    }

    public void breakTourou()
    {
        //灯篭破壊で加点
        calcScore += tourou;
        breakTourouValue++;
        if (breakTourouValue >= clearValue)
        {
            isClear = true;
        }
        else
        _ScoreUI.ScoreUpdate();
    }

    public void Assign(bool isResult)
    {
        if (calcScore >= maxScore)
        {
            calcScore = maxScore;
            return;
        }
        scoreArray[0] = 0;
        scoreArray[1] = 0;
        for (int i = 0; i < calcScore; i++)
        {
            scoreArray[0]++;
            if (scoreArray[0] >= 10)
            {
                scoreArray[1]++;        //スコアが二桁という前提で繰り上げ処理
                scoreArray[0] = 0;
            }
        }

        //スコアに対応した数字の画像を入れる
        scoreImg[tensPlace].sprite = Numbers[scoreArray[1]];
        scoreImg[onePlace].sprite = Numbers[scoreArray[0]];

        //Debug.Log("scoreArray[0] = " + scoreArray[0]);
        //Debug.Log("scoreArray[1] = " + scoreArray[1]);
        //Debug.Log("Numbers[scoreArray[1]] = " + Numbers[scoreArray[1]]);
        //Debug.Log(" scoreImg[tensPlace].sprite = " + scoreImg[tensPlace].sprite);
        //Debug.Log(" scoreImg[onePlace].sprite = " + scoreImg[onePlace].sprite);

        if (isResult == true)   //リザルト画面ならボスの秒数と子だるまの数も入れる
        {
            //bossScoreImg[tensPlace].sprite = Numbers[Boss_Time_Array[1]]; //Numbers[i] i:他スクリプトから取得したスコアを十の位と一の位に分割して入れる
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
       // Debug.Log("SceneName = " + SceneName);
        _FadeAndSceneMove.NextSceneName = SceneName; 　//再挑戦ボタンの移動するシーンの名前を書き換える

        if (calcScore >= 100)
            calcScore = 99;
        //NormalHit();
        //DoubleHit();
        //BeatBoss(10.2f);
       // BeatDaruma();   //1+1+4+20+16, 10秒、01体 良判定
       // calcScore += noDamage;
        //calcScore = num;
        //isNoDmg = true;
        //デバッグ用↑

        Assign(isResult);

        DataMoveAndInit();  //ディレイを作る前にデータ移行と初期化

        yield return new WaitForSeconds(duration);  //始まると同時に表示されるのを防ぐために少し間を空ける

        IndicateScore(scoreImg, indicateS[0]);
        yield return new WaitForSeconds(duration);

        //IndicateScore(bossScoreImg, indicateS[0]);    //ボススコアの表示
        //yield return new WaitForSeconds(duration);

        IndicateScore(kidScoreImg, indicateS[0]);
       // Debug.Log("isNoDmg2 = " + isNoDmg);
        if (noDmgState == true)        //ノーダメだったらチェック入れる
        {
            yield return new WaitForSeconds(duration);
            GameManager.instance.PlaySE(indicateS[0]);
            noDmgImg.enabled = true;
        }
        yield return new WaitForSeconds(duration * 1.5f);
        GameManager.instance.PlaySE(indicateS[1]);
        // result.enabled = true;       

     

        if (clearState == false)  //要調整 調整しやすくする方法わからない
        {
            results[0].enabled = true; //負け
            resultS = resultSounds[0];
        }
        else if (indicateScore < 30)
        {
            results[1].enabled = true; //可
            resultS = resultSounds[1];
        }
        else if (indicateScore >= 30 && indicateScore < 60)
        {
            results[2].enabled = true; //良
            resultS = resultSounds[1];
        }
        else if (indicateScore >= 60 && indicateScore < 80)
        {
            results[3].enabled = true; //優
            resultS = resultSounds[1];
        }
        else if (indicateScore >= 80)
        {
            results[4].enabled = true; //秀
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
        //サウンド
        GameManager.instance.PlaySE(clip);
        Img[tensPlace].enabled = true;
        Img[onePlace].enabled = true;
        
        //若干の遅延
    }

    private void DataMoveAndInit()
    {
        indicateScore = calcScore; indicateDaruma = beatDarumaValue; //indicateTourou = breakTourouValue;

        clearState = isClear;
        noDmgState = isNoDmg;
        isClear = false;
        isNoDmg = false;

        calcScore = 0; beatDarumaValue = 0; breakTourouValue = 0;
    }
    

    public int GetResultScore()
    {
        print("score:"+indicateScore);
        return indicateScore;
    }

    public string GetStageName()
    {
        return InputSceneName;
    }
}
