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
    [SerializeField]
    [Header("大きさをそろえる")]
    bool isSameScale=false;
    [SerializeField]
    [Header("パーティクルをすぐに消す\n" +
        "＊falseの時、パーティクルのlifetimeより早く\n" +
        "呼び出し元が消えると消えなくなります")]
    bool isDeleteImmediately=false;

    public void Play()
    {
        StartCoroutine(yaru());
    }

    IEnumerator yaru()
    {
        print("yaru");
        // パーティクルシステムのインスタンスを生成
        ParticleSystem newParticle = Instantiate(particle);


        if (playPosition == null)
            newParticle.transform.position = this.transform.position;
        else
            newParticle.transform.position = playPosition.position;

        if (isSameRotation)
            newParticle.transform.rotation = this.transform.rotation;
        if (isSameScale)
            newParticle.transform.localScale = this.transform.localScale;


        // パーティクルを発生させる
        newParticle.Play();

        float lifetime = newParticle.main.startLifetimeMultiplier;

        if (isDeleteImmediately)
        {
            Destroy(newParticle.gameObject, lifetime);
        }
        else
        {
            yield return new WaitForSeconds(lifetime);
            print("lifetime:" + lifetime);

            newParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Destroy(newParticle.gameObject, lifetime);

        }
    }


}
