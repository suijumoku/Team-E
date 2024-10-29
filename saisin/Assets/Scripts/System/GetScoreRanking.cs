using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScoreRanking : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI tmProUGUI;

    private string[] ranking = { "�����L���O1��", "�����L���O2��", "�����L���O3��", "�����L���O4��", "�����L���O5��" };
    private int[] rankingValue = new int[5];

    [SerializeField, Header("�\��������e�L�X�g")]
    private string[] rankingText = new string[5];

    void Start()
    {
        tmProUGUI.text = "";
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
