using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("hit");
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
}