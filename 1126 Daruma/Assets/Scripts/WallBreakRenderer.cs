using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreakRenderer : MonoBehaviour
{
    // 当たったものを取ってタグがだるまだったらウォールブレイクカウントを勧める
    // レンダラーのマテリアルを変える
    // 二回目当たったらマテリアルを変えてちょっと（０．５秒とか）待つ
    // エフェクトを出しつつ消える
    // Start is called before the first frame update

    [SerializeField]
    [Header("壁の破壊差分")]
    private Material[] WallMaterial = new Material[2];

    [SerializeField]
    [Header("壊れるまでの待ち時間")]
    private float StayBreakTime = 0;

    [SerializeField]
    [Header("再生するパーティクル")]
    private ParticlePlayer[] particlePlayer;


    [SerializeField]
    int breakCount = -1;
    float time = 0;
    Renderer nowMaterial;


    // Update is called once per frame

    private void Start()
    {
        nowMaterial = GetComponent<Renderer>();
    }
    void Update()
    {
        if (breakCount >= 1)
        {
            if (time == 0)
            {
                for(int i=0;i<particlePlayer.Length;i++)
                {
                    particlePlayer[i].Play();

                }
            }
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
            nowMaterial.material = WallMaterial[breakCount];

        }
    }
}
