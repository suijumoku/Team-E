using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class GetAllRankingScore : MonoBehaviour
{
    const string STAGENAME_UME = "Stage_ume";
    const string STAGENAME_TAKE = "Stage_take";
    const string STAGENAME_MATSU = "Stage_matsu";

    [SerializeField, Header("î~")]
    private string[] ranking_ume;
    [SerializeField]
    private TextMeshProUGUI tmProUGUI_ume;

    [SerializeField, Header("í|")]
    private string[] ranking_take;
    [SerializeField]
    private TextMeshProUGUI tmProUGUI_take;

    [SerializeField, Header("èº")]
    private string[] ranking_matsu;
    [SerializeField]
    private TextMeshProUGUI tmProUGUI_matsu;

    private string[] _InitRankingText
    {
        get { return new string[] { "1à ", "2à ", "3à ", "4à ", "5à " }; }
    }

    void Awake()
    {
        InitText();

        GetRankingScore();

        DisplayRanking();
    }

    void GetRankingScore()
    {
        //ÉâÉìÉLÉìÉOåƒÇ—èoÇµ
        for (int i = 0; i < 5; i++)
        {
            string text = "";

            text = PlayerPrefs.GetInt(STAGENAME_UME + ranking_ume[i], 0).ToString("d3");
            ranking_ume[i] += ":" + text + "\n";



            text = PlayerPrefs.GetInt(STAGENAME_TAKE + ranking_take[i], 0).ToString("d3");
            ranking_take[i] += ":" + text + "\n";


            text = PlayerPrefs.GetInt(STAGENAME_MATSU + ranking_matsu[i], 0).ToString("d3");
            ranking_matsu[i] += ":" + text + "\n";

        }

    }

    private void DisplayRanking()
    {
        for (int i = 0; i < 5; i++)
        {
            tmProUGUI_ume.text += ranking_ume[i];
            tmProUGUI_take.text += ranking_take[i];
            tmProUGUI_matsu.text += ranking_matsu[i];

        }
    }

    void InitRankingScore()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(STAGENAME_UME + ranking_ume[i], 0);
            PlayerPrefs.SetInt(STAGENAME_TAKE + ranking_take[i], 0);
            PlayerPrefs.SetInt(STAGENAME_MATSU + ranking_matsu[i], 0);
        }
    }

    void InitText()
    {
        tmProUGUI_ume.text = "";
        tmProUGUI_take.text = "";
        tmProUGUI_matsu.text = "";

        ranking_ume = _InitRankingText;
        ranking_take = _InitRankingText;
        ranking_matsu = _InitRankingText;
    }

    public void ResetRanking()
    {
        PlayerPrefs.DeleteAll();
        InitText();
        GetRankingScore();
        DisplayRanking();
    }
}


