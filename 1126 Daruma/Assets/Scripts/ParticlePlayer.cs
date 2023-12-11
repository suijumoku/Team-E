using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    private ParticleSystem particle;
    [SerializeField]
    [Header("�p�[�e�B�N�����o���ꏊ")]
    Transform playPosition;
    [SerializeField]
    [Header("���������낦��")]
    bool isSameRotation=false;
    [SerializeField]
    [Header("�傫�������낦��")]
    bool isSameScale=false;
    [SerializeField]
    [Header("�p�[�e�B�N���������ɏ���\n" +
        "��false�̎��A�p�[�e�B�N����lifetime��葁��\n" +
        "�Ăяo������������Ə����Ȃ��Ȃ�܂�")]
    bool isDeleteImmediately=false;

    public void Play()
    {
        StartCoroutine(yaru());
    }

    IEnumerator yaru()
    {
        print("yaru");
        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�
        ParticleSystem newParticle = Instantiate(particle);


        if (playPosition == null)
            newParticle.transform.position = this.transform.position;
        else
            newParticle.transform.position = playPosition.position;

        if (isSameRotation)
            newParticle.transform.rotation = this.transform.rotation;
        if (isSameScale)
            newParticle.transform.localScale = this.transform.localScale;


        // �p�[�e�B�N���𔭐�������
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
