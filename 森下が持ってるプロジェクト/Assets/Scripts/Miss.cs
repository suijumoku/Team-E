//古澤
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miss : MonoBehaviour
{
    [SerializeField] private ChangeImage _ChangeImage1, _ChangeImage2, _ChangeImage3;

    private int failC = 0;

    //OutScriptとPlayerControllerからMissにアクセス->changeImageにアクセスしてライフを減らす

    public void InOrder()  //これを他スクリプトで呼び出せば体力減らせる
    {
        if (failC == 0)
        {
            _ChangeImage1.StartCoroutine("Miss");
            failC++;
            _ChangeImage1 = null;
        }
        else if (failC == 1)
        {
            _ChangeImage2.StartCoroutine("Miss");
            failC++;
            _ChangeImage2 = null;
        }
        else if (failC == 2)
        {
            _ChangeImage3.StartCoroutine("Miss");
            failC++;
            _ChangeImage3 = null;
        }
    }
}
