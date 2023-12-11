using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingLanternBreakRenderer : MonoBehaviour
{
    [SerializeField]
    [Header("����܂ł̑҂�����")]
    private float StayBreakTime = 0;

    [SerializeField]
    int breakCount = -1;
    float time = 0;
    [SerializeField]
    [Header("�Đ�����p�[�e�B�N��")]
    private ParticlePlayer particlePlayer;

    void Update()
    {
        if (breakCount >= 0)
        {
            if (time == 0)
                particlePlayer.Play();
            time += Time.deltaTime;
            if (StayBreakTime < time)
            {
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