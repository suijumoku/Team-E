//古澤
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MaterialPropetyBlockを使うと同じマテリアルを使用している
//複数のオブジェクトに対して、それぞれ独自のプロパティ値を設定できる



public class Semitransparent : MonoBehaviour
{
    [SerializeField] float alphaValue = 0.5f;

    private Color color = Color.white;

    //親 子オブジェクトを格納。
    private MeshRenderer[] meshRenderers;
    private MaterialPropertyBlock m_mpb;

    //一つのマテリアルを共有していてもそれぞれ独自に設定をいじれる
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
      
    public void ClearMaterialInvoke()
    {
        color.a = alphaValue;

        //Corolのアルファ値を設定した後その色を"_Color"として(たぶん) MaterialPropertyBlockに設定
        //SetColorよりもPropertyToIDのほうが処理が速いらしい

        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }
    public void NotClearMaterialInvoke()
    {
        //元に戻す
        //参考資料だと.bになっていたが多分ミス。.aで動いたがたまにマテリアルが一瞬水色になった。
        color.a = 1f;        

        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }

 }
