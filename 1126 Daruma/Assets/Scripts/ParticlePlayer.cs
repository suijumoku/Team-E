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

    public void Play()
    {
        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�
        ParticleSystem newParticle = Instantiate(particle);
        

        if(playPosition==null)
        newParticle.transform.position = this.transform.position;
        else
        newParticle.transform.position = playPosition.position;

        if (isSameRotation)
        newParticle.transform.rotation = this.transform.rotation;
        // �p�[�e�B�N���𔭐�������
        newParticle.Play();

        float lifetime = newParticle.main.startLifetimeMultiplier;

        // �p�[�e�B�N�����Đ����������
        Destroy(newParticle.gameObject, lifetime);
    }
}
