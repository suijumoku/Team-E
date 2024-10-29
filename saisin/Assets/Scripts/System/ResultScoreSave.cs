using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScoreSave : MonoBehaviour
{
    [SerializeField]
    ResultManager resultManager;

    [SerializeField, Header("セーブするスコア")]
    private int score;

    private string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    private int[] rankingValue = new int[5];

    [SerializeField, Header("表示させるテキスト")]
    private string[] rankingText = new string[5];
    // Start is called before the first frame update
    void Start()
    {
        score = resultManager.GetResultScore();
        GetRanking();
        SetRanking(score);
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }


    void GetRanking()
    {
        print("ランキング呼び出し");
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i], 0);
        }
    }
    /// <summary>
    /// ランキング書き込み
    /// </summary>
    void SetRanking(int _value)
    {
        print("ランキングにセット");
        // 5しかないのでバブルソートでいい
        for (int i = 0; i < ranking.Length; i++)
        {
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
            }
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }

}
