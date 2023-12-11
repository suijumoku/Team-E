//���V
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MaterialPropetyBlock���g���Ɠ����}�e���A�����g�p���Ă���
//�����̃I�u�W�F�N�g�ɑ΂��āA���ꂼ��Ǝ��̃v���p�e�B�l��ݒ�ł���

public class Semitransparent : MonoBehaviour
{
    private Color color = Color.white;

    //�e �q�I�u�W�F�N�g���i�[�B
    private MeshRenderer[] meshRenderers;
    private MaterialPropertyBlock m_mpb;

    public MaterialPropertyBlock mpb
    {
        //??��null���̉��Z�q�B���ӂ� null �łȂ��ꍇ�͍��ӂ̒l���A
        //null �̏ꍇ�͉E�ӂ̒l��Ԃ��B

        // �܂�m_mpb��null�̏ꍇ�ɂ̂ݐV����MaterialPropertyBlock���쐬�B
        get { return m_mpb ?? (m_mpb = new MaterialPropertyBlock()); }
    }

    void Awake()
    {
        meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
    }
  
    // Update is called once per frame
    void Update()
    {

    }

    public void ClearMaterialInvoke()
    {
        color.a = 0.15f;

        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse ZWrite");
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }
    public void NotClearMaterialInvoke()
    {
        color.b = 1f;
        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Diffuse");
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }

 }
