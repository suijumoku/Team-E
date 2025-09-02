using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingLanternBreakRenderer : MonoBehaviour
{
    [SerializeField]
    [Header("�I���܂ł̑҂�����")]
    private float StayBreakTime = 0;  
    [SerializeField]
    [Header("�I�����ɃA�^�b�`���ꂽ�I�u�W�F�N�g���������ǂ���")]
    private bool isDestroy=false;

    [SerializeField]
    int breakCount = -1;
    float time = 0;
    [SerializeField]
    [Header("�Đ�����p�[�e�B�N��")]
    private ParticlePlayer particlePlayer;

    [SerializeField] ResultManager resultManager = default!;
    GameObject obj = default;

    [SerializeField] AudioClip[] breaktourou = default!;

    private void Awake()
    {
        obj = GameObject.Find("ResultManager");
        resultManager = obj.GetComponent<ResultManager>();
    }

    void Update()
    {
        if (breakCount >= 0)
        {
            if (time <= 0)
                particlePlayer.Play();
            time += Time.deltaTime;
            if (StayBreakTime < time)
            {
                Debug.Log("break!");
                GameManager.instance.PlaySE(breaktourou[0]);               
                if (isDestroy)
                {
                    Destroy(gameObject);
                    resultManager.BreakTourou();
                }                   
                else
                    Destroy(this);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DarumaBall"))
        {
            print("hit");
            breakCount++;
        }
    }
}
