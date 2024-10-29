using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScoreRanking : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI tmProUGUI;

    private string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    private int[] rankingValue = new int[5];

    [SerializeField, Header("表示させるテキスト")]
    private string[] rankingText = new string[5];

    void Start()
    {
        tmProUGUI.text = "";
        GetRanking();

        DisplayRanking();
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    private void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i],0);
        }
    }

    private void DisplayRanking()
    {
        for (int i = 0; i < rankingText.Length; i++)
        {
            string text = $"{ranking[i]}:{rankingValue[i].ToString()}\n";
            tmProUGUI.text += text;
            rankingText[i] = text;
        }
    }
}
