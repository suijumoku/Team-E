using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //[Header("スコア")]
    [SerializeField] Image[][] score;  //[i][j] i:親か子 j:十の位か一の位
    [Header("スコアの数字")]
    [SerializeField] Sprite[] Numbers;
    [Header("リザルト画像たち")]
    [SerializeField] Sprite[] results;
    [Header("表示時のサウンド")]
    [SerializeField] AudioClip[] indicateS = default!;
    [Header("表彰音")]
    [SerializeField] AudioClip clapS = default!;

    [SerializeField] AudioClip BGM = default!;
    private Image result;

    const int boss = 0, kid = 1;
    const int tensPlace = 0, onePlace = 1;
    const float duration = 0.5f;


    void Start()
    {
        //GetScore見たいな関数作ればよさそう

        score[boss][tensPlace].sprite = Numbers[0]; //Numbers[i] i:他スクリプトから取得したスコアを十の位と一の位に分割して入れる
        score[boss][onePlace].sprite = Numbers[0];

        score[kid][tensPlace].sprite = Numbers[0];
        score[kid][onePlace].sprite = Numbers[0];

        result.sprite = results[1]; //results[i] iの値を変える
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Indicate()
    {

        for (int i = 0; i < 2; i++)
        {
            //サウンド
            GameManager.instance.PlaySE(indicateS[1]);
            score[i][tensPlace].enabled = true;
            score[i][onePlace].enabled = true;
            new WaitForSeconds(duration);
            //若干の遅延
        }

        GameManager.instance.PlaySE(indicateS[2]);
        result.enabled = true;
    }
}
