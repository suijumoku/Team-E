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

    [SerializeField, Header("�X�e�[�W��")]
    private string stageName;

    private string[] ranking = { "1��", "2��", "3��", "4��", "5��" };
    private int[] rankingValue = new int[5];

    [SerializeField, Header("�\��������e�L�X�g")]
    private string[] rankingText = new string[5];

    void Start()
    {
        tmProUGUI.text = "";
        stageName=resultManager.GetStageName();
        GetRanking();

        DisplayRanking();
    }

    /// <summary>
    /// �����L���O�Ăяo��
    /// </summary>
    private void GetRanking()
    {
        //�����L���O�Ăяo��
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
