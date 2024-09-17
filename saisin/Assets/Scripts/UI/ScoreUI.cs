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
        _ResultManager.scoreImg[1].enabled = true;  //最初は00で表示
    }

    public void ScoreUpdate()   //相互にアクセスしてるの良くない
    {
        _ResultManager.Assign(isResult);    //スコアが加算されるたびに更新
    }

}
