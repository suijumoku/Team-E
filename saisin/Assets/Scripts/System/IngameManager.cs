using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
    [Header("シーンが読み込まれたら実行する")]
    [SerializeField] bool OnLoadScene;

    [Header("時間(3桁)")]
    [SerializeField] Image[] timeImg;

    [SerializeField]
    [Header("終わった時に流すアニメーション")]
    private EndAnimPlayer endAnim;
    [SerializeField]
    [Header("終わった時に表示する画像(失敗)")]
    private Image loseImage;
    [SerializeField]
    [Header("終わった時に表示する画像(成功)")]
    private Image winImage;

    [SerializeField] FadeAndSceneMove _fadeAndSceneMove;

    [SerializeField] int missCount = default!;
    [SerializeField] private BlinkingObject blinkingObject = default!;
    [SerializeField] ResultManager resultManager = default!;

    [SerializeField] AudioClip loseS = default!;
    [SerializeField] AudioClip winS = default!;

    public bool isDefeat = false, isInvincible = false; //無敵時間中かどうか

    int[] timeArray;
    int currentCount = 0;
    [SerializeField]
    float time = 0f, beforeTime, floarTime;
    GameObject obj = default;
    bool onlyF = false;


    void Start()
    {
        if (OnLoadScene)
        {
            onlyF = false;
            isDefeat = false;
            timeArray = new int[3] { 0, 0, 0 };
            currentCount = 0;
            time = -3f;
        }
        loseImage.gameObject.SetActive(false);
        winImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (ResultManager.isClear == true && onlyF == false || UnityEngine.Input.GetKeyDown(KeyCode.K) && onlyF == false)
        {
            ResultManager.isClear = true;  //デバッグ用 
            resultManager.BeatBoss(floarTime);   //本来はボス撃破時の関数だがボス実装断念により、クリアタイムボーナスとして呼ぶ
            StartCoroutine(Clear());
            onlyF = true;
        }
      
        obj = GameObject.Find("ResultManager");
        resultManager = obj.GetComponent<ResultManager>();  //resultmanagerの更新

        if (UnityEngine.Input.GetKeyDown(KeyCode.P))    //デバッグ用、Pでリザルトへ
        {
           StartCoroutine(Defeat());
        }
        
   
        time += Time.deltaTime;

        floarTime = Mathf.Floor(time);   //切り捨て

        if (floarTime - beforeTime >= 1.0f&&time>0f) //1フレーム前の時間から変化していたら(1秒経過したら)繰り上げ処理
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
            StartCoroutine(Defeat());
            isDefeat = false;
        }
        beforeTime = floarTime;
    }
    public void Miss()
    {
        if (currentCount >= missCount) return;

        InOrder(currentCount);
        currentCount++;
    }

    public void InOrder(int i)
    {
        blinkingObject.StartCoroutine(blinkingObject.DamageIndication(i));
    }

    private IEnumerator Defeat()
    {
        endAnim.Play();
        yield return new WaitForSeconds(1f);
        
        GameManager.instance.PlaySE(loseS);
        endAnim.gameObject.SetActive(false);
        loseImage.gameObject.SetActive(true);       
        
        yield return new WaitForSeconds(1f);
        _fadeAndSceneMove.FadeStart();
    }

    private IEnumerator Clear()
    {
        obj = GameObject.Find("Life");
        blinkingObject = obj.GetComponent<BlinkingObject>();
        
        if (blinkingObject.life == 3)
        {
            resultManager.NoDmgBonus(); //ライフが３残ってたらノーダメボーナス
        }

        endAnim.Play();
        
        yield return new WaitForSeconds(1f);
        
        GameManager.instance.PlaySE(winS);
        endAnim.gameObject.SetActive(false);
        winImage.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        _fadeAndSceneMove.FadeStart();
    }
}
