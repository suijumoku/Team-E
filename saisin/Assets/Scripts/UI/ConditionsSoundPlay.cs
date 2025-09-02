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
            GameManager.instance.PlaySE(sound);
        }
    }

    public void OnClickPlay()
    {
        GameManager.instance.PlaySE(sound);
    }
}
