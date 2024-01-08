using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimPlayer : MonoBehaviour
{

    [SerializeField]
    [Header("�Đ�����A�j���[�V�����N���b�v")]
    private AnimationClip clip;

    [SerializeField]
    [Header("�G���hSE")]
    private AudioClip EndClip;

    private Animator animator;
    private new string name;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        name = EndClip.name;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Play()
    {
        this.gameObject.SetActive(true);
        GameManager.instance.InputStateOff();
        animator.Play(name);
    }

    public void EndSE()
    {
        GameManager.instance.PlaySE(EndClip);
    }
}
