using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class GetAllRankingScore : MonoBehaviour
{
    const string STAGENAME_MATSU = "Stage_matsu";
    const string STAGENAME_TAKE = "Stage_take";
    const string STAGENAME_UME = "Stage_ume";

    [SerializeField, Header("èº")]
    private string[] ranking_matsu = {"1à ", "2à ", "3à ", "4à ", "5à " };
    [SerializeField]
    private TextMeshProUGUI tmProUGUI_matsu;
    [SerializeField, Header("í|")]
    private string[] ranking_take = {"1à ", "2à ", "3à ", "4à ", "5à " };
    [SerializeField]
    private TextMeshProUGUI tmProUGUI_take;
    [SerializeField, Header("î~")]
    private string[] ranking_ume = {"1à ", "2à ", "3à ", "4à ", "5à " };
    [SerializeField]
    private TextMeshProUGUI tmProUGUI_ume;

    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        tmProUGUI_matsu.text = "";
        tmProUGUI_take.text = "";
        tmProUGUI_ume.text = "";

        GetRankingScore();

        DisplayRanking();
    }

    void GetRankingScore()
    {
        //ÉâÉìÉLÉìÉOåƒÇ—èoÇµ
        for (int i = 0; i < 5; i++)
        {
            string text="";
            print(STAGENAME_MATSU + ranking_matsu[i]);
            text=  PlayerPrefs.GetInt(STAGENAME_MATSU + ranking_matsu[i], 0).ToString("d3");
            ranking_matsu[i] += ":" + text+"\n";

            print(STAGENAME_TAKE + ranking_take[i]);
            text= PlayerPrefs.GetInt(STAGENAME_TAKE + ranking_take[i], 0).ToString("d3");
            ranking_take[i] += ":" + text + "\n";

            print(STAGENAME_UME + ranking_ume[i]);
            text= PlayerPrefs.GetInt(STAGENAME_UME + ranking_ume[i], 0).ToString("d3");
            ranking_ume[i] += ":" + text + "\n";
        }
    }

    private void DisplayRanking()
    {
        for (int i = 0; i < 5; i++)
        {
            tmProUGUI_matsu.text +=ranking_matsu[i];
            tmProUGUI_take.text +=ranking_take[i];
            tmProUGUI_ume.text +=ranking_ume[i];
        }
    }

    void InitRankingScore()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(STAGENAME_MATSU + ranking_matsu[i], 0);
            PlayerPrefs.SetInt(STAGENAME_TAKE + ranking_take[i], 0);
            PlayerPrefs.SetInt(STAGENAME_UME + ranking_ume[i], 0);
        }
    }
}
