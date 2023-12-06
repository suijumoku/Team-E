using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    private ParticleSystem particle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("hit");
            // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�
            ParticleSystem newParticle = Instantiate(particle);
            // �p�[�e�B�N�����A�^�b�`�����I�u�W�F�N�g�Ɠ����ꏊ�A�����ɂ���
            newParticle.transform.position = this.transform.position;
            newParticle.transform.rotation = this.transform.rotation;
            // �p�[�e�B�N���𔭐�������
            newParticle.Play();

            float lifetime = newParticle.main.startLifetimeMultiplier;

            // �p�[�e�B�N�����Đ����������
            Destroy(newParticle.gameObject, lifetime);
        }
      
    }
}