using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarumaHeadScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] ResultManager resultManager;
    GameObject obj = default;

    [SerializeField]
    [Header("壊れるまでの待ち時間")]
    private float StayBreakTime = 0;

    [SerializeField]
    int breakCount = -1;
    float time = 0;
    [SerializeField]
    [Header("再生するパーティクル")]
    private ParticlePlayer particlePlayer;

    void Start()
    {
     
    }

    // Update is called once per frame
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
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("b");
            print("hit");
            breakCount++;
        }
    }
}
