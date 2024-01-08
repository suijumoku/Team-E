using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDownPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Header("�Đ�����A�j���[�V�����N���b�v")]
    private AnimationClip clip;

    [SerializeField]
    [Header("�J�E���gSE")]
    private AudioClip CountClip;
    [SerializeField]
    [Header("�X�^�[�gSE")]
    private AudioClip StartClip;
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

    public void CountSE()
    {
        GameManager.instance.PlaySE(CountClip);
        GameManager.instance.InputStateOff();
    }

    public void StartSE()
    {
        GameManager.instance.PlaySE(StartClip);
        GameManager.instance.InputStateOn();
        GameManager.instance.SetCurrentState(GameState.NormalEnemyBattle);

    }
}
