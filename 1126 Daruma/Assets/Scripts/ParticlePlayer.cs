using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;
    [SerializeField]
    [Header("パーティクルを出す場所")]
    Transform playPosition;
    [SerializeField]
    [Header("向きをそろえる")]
    bool isSameRotation=false;

    public void Play()
    {
        // パーティクルシステムのインスタンスを生成
        ParticleSystem newParticle = Instantiate(particle);
        

        if(playPosition==null)
        newParticle.transform.position = this.transform.position;
        else
        newParticle.transform.position = playPosition.position;

        if (isSameRotation)
        newParticle.transform.rotation = this.transform.rotation;
        // パーティクルを発生させる
        newParticle.Play();

        float lifetime = newParticle.main.startLifetimeMultiplier;

        // パーティクルを再生したら消す
        Destroy(newParticle.gameObject, lifetime);
    }
}
