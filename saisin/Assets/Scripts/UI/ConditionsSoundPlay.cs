using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConditionsSoundPlay : MonoBehaviour
{

    [Header("�L���ɂȂ�������s����")]
    [SerializeField] bool OnEneble;
    [Header("�Đ�����T�E���h")]
    [SerializeField] AudioClip sound = default!;
    void OnEnable()
    {
        if (OnEneble)
        {
            Debug.Log("OnEnable, PlaySE");
            GameManager.instance.PlaySE(sound);
        }
    }

    public void OnClickPlay()
    {
        Debug.Log("OnClick, PlaySE");
        GameManager.instance.PlaySE(sound);
    }
}
