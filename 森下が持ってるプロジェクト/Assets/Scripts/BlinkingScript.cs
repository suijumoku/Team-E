using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingScript : MonoBehaviour
{
    [SerializeField] GameObject player = default!;
    [Header("�\���ꏊ")]
    [SerializeField] Image[] lifeImage = default!;
    [Header("�ʏ펞�摜")]
    [SerializeField] Sprite truelife = default!;
    [Header("�_���[�W���摜")]
    [SerializeField] Sprite falselife = default!;
    [Header("�ʏ펞�}�e���A��")]
    [SerializeField] Material trueMaterial = default!;
    [Header("�_���[�W���}�e���A��")]
    [SerializeField] Material falseMaterial = default!;
    [Header("�_���[�W���̕\���Ԋu")]
    [SerializeField] float[] duration = default!;


    void Awake()
    {
        //�ŏ��ɑS�Ă�Life�摜��true��
        foreach (Image t in lifeImage)
        {
            t.enabled = truelife;
        }
    }

    //number�F�\���摜�ԍ� x�F���������
    void lifeChange(int number, int x)
    {
        if (x % 2 == 0)
        {
            lifeImage[number].sprite = falselife;
            player.gameObject.GetComponent<Renderer>().material = falseMaterial;
        }
        else
        {
            lifeImage[number].sprite = truelife;
            player.gameObject.GetComponent<Renderer>().material = trueMaterial;
        }
    }

    public IEnumerator DamageIndication(int i)
    {
        yield return new WaitForSeconds(0.15f);
        //WaitForSeconds�ł��ꂼ��ҋ@���Ă���LifeChange���s��
        for (int j = 0; j < duration.Length; j++)
        {
            lifeChange(i, j);
            yield return new WaitForSeconds(duration[j]);
        }

        //�Ō�͌��炳�Ȃ���΂Ȃ�Ȃ��̂�false��
        lifeImage[i].sprite = falselife;

        //�v���C���[�̃}�e���A����ʏ�ɁB���C�t�����_�ł̉񐔂͑����Ă��܂�
       // yield return new WaitForSeconds(0.1f);
        player.gameObject.GetComponent<Renderer>().material = trueMaterial;     
        yield return null;
    }

}
