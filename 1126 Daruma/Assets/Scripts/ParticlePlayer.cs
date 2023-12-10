using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;

    public void Play()
    {
        // パーティクルシステムのインスタンスを生成
        ParticleSystem newParticle = Instantiate(particle);
        // パーティクルをアタッチしたオブジェクトと同じ場所、向きにする
        newParticle.transform.position = this.transform.position;
        newParticle.transform.rotation = this.transform.rotation;
        // パーティクルを発生させる
        newParticle.Play();

        float lifetime = newParticle.main.startLifetimeMultiplier;

        // パーティクルを再生したら消す
        Destroy(newParticle.gameObject, lifetime);
    }
}
