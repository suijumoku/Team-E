using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScoreRanking : MonoBehaviour
{
    [SerializeField]
    private ResultManager resultManager;

    [SerializeField]
    private TextMeshProUGUI tmProUGUI;

    [SerializeField, Header("ステージ名")]
    private string stageName;

    private string[] ranking = { "1位", "2位", "3位", "4位", "5位" };
    private int[] rankingValue = new int[5];

    [SerializeField, Header("表示させるテキスト")]
    private string[] rankingText = new string[5];

    void Start()
    {
        tmProUGUI.text = "";
        stageName=resultManager.GetStageName();
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
            rankingValue[i] = PlayerPrefs.GetInt(stageName+ranking[i],0);
        }
    }

    private void DisplayRanking()
    {
        for (int i = 0; i < rankingText.Length; i++)
        {
            string text = $"{ranking[i]}:{rankingValue[i].ToString("d3")}\n";
            tmProUGUI.text += text;
            rankingText[i] = text;
        }
    }
}
