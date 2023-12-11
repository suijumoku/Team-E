//古澤
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MaterialPropetyBlockを使うと同じマテリアルを使用している
//複数のオブジェクトに対して、それぞれ独自のプロパティ値を設定できる

public class Semitransparent : MonoBehaviour
{
    private Color color = Color.white;

    //親 子オブジェクトを格納。
    private MeshRenderer[] meshRenderers;
    private MaterialPropertyBlock m_mpb;

    public MaterialPropertyBlock mpb
    {
        //??はnull合体演算子。左辺が null でない場合は左辺の値を、
        //null の場合は右辺の値を返す。

        // つまりm_mpbがnullの場合にのみ新しいMaterialPropertyBlockを作成。
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
