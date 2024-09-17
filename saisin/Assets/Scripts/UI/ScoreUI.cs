using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] ResultManager _ResultManager = default!;
    bool isResult = false; 

    void Start()
    {
        _ResultManager.Assign(isResult);
        _ResultManager.scoreImg[0].enabled = true;
        _ResultManager.scoreImg[1].enabled = true;  //�ŏ���00�ŕ\��
    }

    public void ScoreUpdate()   //���݂ɃA�N�Z�X���Ă�̗ǂ��Ȃ�
    {
        _ResultManager.Assign(isResult);    //�X�R�A�����Z����邽�тɍX�V
    }

}
