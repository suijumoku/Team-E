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
    private Image result;


    const int boss = 0, kid = 1;
    const int tensPlace = 0, onePlace = 1;


    void Start()
    {
        //GetScore見たいな関数作ればよさそう

        score[boss][tensPlace].sprite = Numbers[0]; //Numbers[i] i:他スクリプトから取得したスコアを十の位と一の位に分割して入れる
        score[boss][onePlace].sprite = Numbers[0];

        score[kid][tensPlace].sprite = Numbers[0];
        score[kid][onePlace].sprite = Numbers[0];

        result.sprite = results[1]; //results[i] iの値を変える

        for (int i = 0; i < 2; i++)
        {
            //サウンド
            for (int j = 0; j < 2; j++)
            {
                score[i][j].enabled = true;
            }
            //若干の遅延
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
