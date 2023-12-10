using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreakRenderer : MonoBehaviour
{
    // �����������̂�����ă^�O������܂�������E�H�[���u���C�N�J�E���g�����߂�
    // �����_���[�̃}�e���A����ς���
    // ���ړ���������}�e���A����ς��Ă�����Ɓi�O�D�T�b�Ƃ��j�҂�
    // �G�t�F�N�g���o��������
    // Start is called before the first frame update

    [SerializeField]
    [Header("�ǂ̔j�󍷕�")]
    private Material[] WallMaterial = new Material[2];

    [SerializeField]
    [Header("����܂ł̑҂�����")]
    private float StayBreakTime = 0;

    [SerializeField]
    [Header("�Đ�����p�[�e�B�N��")]
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
