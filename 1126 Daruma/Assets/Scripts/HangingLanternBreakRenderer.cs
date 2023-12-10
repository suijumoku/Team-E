using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingLanternBreakRenderer : MonoBehaviour
{
    [SerializeField]
    [Header("‰ó‚ê‚é‚Ü‚Å‚Ì‘Ò‚¿ŽžŠÔ")]
    private float StayBreakTime = 0;

    [SerializeField]
    int breakCount = -1;
    float time = 0;
    ParticlePlayer particlePlayer;


    // Update is called once per frame

    private void Start()
    {
        particlePlayer = GetComponent<ParticlePlayer>();
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
