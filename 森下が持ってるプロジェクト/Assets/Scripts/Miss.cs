//���V
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miss : MonoBehaviour
{
    [SerializeField] private ChangeImage _ChangeImage1, _ChangeImage2, _ChangeImage3;

    private int failC = 0;

    //OutScript��PlayerController����Miss�ɃA�N�Z�X->changeImage�ɃA�N�Z�X���ă��C�t�����炷

    public void InOrder()  //����𑼃X�N���v�g�ŌĂяo���Α̗͌��点��
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
