using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConditionsSoundPlay : MonoBehaviour
{

    [Header("有効になったら実行する")]
    [SerializeField] bool OnEneble;
    [Header("再生するサウンド")]
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
