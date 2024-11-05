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
    [SerializeField, Header("�X�e�[�W��")]
    private string stageName;

    private string[] ranking = { "1��", "2��", "3��", "4��", "5��" };
    private int[] rankingValue = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        score = resultManager.GetResultScore();
        stageName=resultManager.GetStageName();
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
            rankingValue[i] = PlayerPrefs.GetInt(stageName+ranking[i], 0);
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
            print(stageName + ranking[i]);
            PlayerPrefs.SetInt(stageName+ranking[i], rankingValue[i]);
        }
    }

}
