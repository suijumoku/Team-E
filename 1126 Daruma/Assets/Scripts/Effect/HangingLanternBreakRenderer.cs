using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingLanternBreakRenderer : MonoBehaviour
{
    [SerializeField]
    [Header("壊れるまでの待ち時間")]
    private float StayBreakTime = 0;

    [SerializeField]
    int breakCount = -1;
    float time = 0;
    [SerializeField]
    [Header("再生するパーティクル")]
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
            if (time == 0)
                particlePlayer.Play();
            time += Time.deltaTime;
            if (StayBreakTime < time)
            {
                GameManager.instance.PlaySE(breaktourou[0]);
                resultManager.breakTourou();
                Destroy(gameObject);
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
