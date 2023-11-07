using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentionScript : MonoBehaviour
{
    [SerializeField] private ChangeImage _ChangeImage1, _ChangeImage2, _ChangeImage3;

    private int failC = 0;

    public void InOrder_() 
    {
        if (failC == 0)
        {
            _ChangeImage1.StartCoroutine("Die");
            failC++;
            _ChangeImage1 = null;
        }
        else if (failC == 1)
        {
            _ChangeImage2.StartCoroutine("Die");
            failC++;
            _ChangeImage2 = null;
        }
        else if (failC == 2)
        {
            _ChangeImage3.StartCoroutine("Die");
            failC++;
            _ChangeImage3 = null;
        }
    }
}
