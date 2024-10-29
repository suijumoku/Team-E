using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScoreSave : MonoBehaviour
{
    [SerializeField]
    ResultManager resultManager;

    [SerializeField, Header("�Z�[�u����X�R�A")]
    private int score;

    private string[] ranking = { "�����L���O1��", "�����L���O2��", "�����L���O3��", "�����L���O4��", "�����L���O5��" };
    private int[] rankingValue = new int[5];

    [SerializeField, Header("�\��������e�L�X�g")]
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
        print("�����L���O�Ăяo��");
        //�����L���O�Ăяo��
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i], 0);
        }
    }
    /// <summary>
    /// �����L���O��������
    /// </summary>
    void SetRanking(int _value)
    {
        print("�����L���O�ɃZ�b�g");
        // 5�����Ȃ��̂Ńo�u���\�[�g�ł���
        for (int i = 0; i < ranking.Length; i++)
        {
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
            }
        }

        //����ւ����l��ۑ�
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }

}
