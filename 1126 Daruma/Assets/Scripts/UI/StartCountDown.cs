using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Header("�Đ�����A�j���[�V�����N���b�v")]
    private AnimationClip clip;
    private Animator animator;

    private void Start()
    {
        Time.timeScale = 0f;
        animator =GetComponent<Animator>();
        string name = clip.name;
        animator.Play(name);
    }
    public void OnAnimationEnd()
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
}
