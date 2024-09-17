//���V
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MaterialPropetyBlock���g���Ɠ����}�e���A�����g�p���Ă���
//�����̃I�u�W�F�N�g�ɑ΂��āA���ꂼ��Ǝ��̃v���p�e�B�l��ݒ�ł���



public class Semitransparent : MonoBehaviour
{
    [SerializeField] float alphaValue = 0.5f;

    private Color color = Color.white;

    //�e �q�I�u�W�F�N�g���i�[�B
    private MeshRenderer[] meshRenderers;
    private MaterialPropertyBlock m_mpb;

    //��̃}�e���A�������L���Ă��Ă����ꂼ��Ǝ��ɐݒ���������
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
      
    public void ClearMaterialInvoke()
    {
        color.a = alphaValue;

        //Corol�̃A���t�@�l��ݒ肵���セ�̐F��"_Color"�Ƃ���(���Ԃ�) MaterialPropertyBlock�ɐݒ�
        //SetColor����PropertyToID�̂ق��������������炵��

        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }
    public void NotClearMaterialInvoke()
    {
        //���ɖ߂�
        //�Q�l��������.b�ɂȂ��Ă����������~�X�B.a�œ����������܂Ƀ}�e���A������u���F�ɂȂ����B
        color.a = 1f;        

        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }

 }
