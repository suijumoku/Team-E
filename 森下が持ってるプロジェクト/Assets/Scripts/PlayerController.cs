using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    private Vector3 velocity;
    private Vector3 input;

    Animator animator;
    AnimatorStateInfo stateInfo;

    [SerializeField] private LayerMask groundLayers;    //�n�ʂ̏�Ȃ�������[�V�����A�Ⴄ�Ȃ痎�����[�V����    
    [SerializeField] private float walkSpeed = 4f;  //�ړ��X�s�[�h
    [SerializeField] private bool isGrouded;        //�ڒn���Ă��邩�ǂ���

    ////�@�ڒn�m�F�̃R���C�_�̈ʒu�̃I�t�Z�b�g
    //[SerializeField]
    //private Vector3 groundPositionOffset = new Vector3(0f, 0.02f, 0f);
    ////�@�ڒn�m�F�̋��̃R���C�_�̔��a
    //[SerializeField]
    //private float groundColliderRadius = 0.29f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CheckGround();

        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //�ړ����x�̌v�Z

        var clampedInput = Vector3.ClampMagnitude(input, 1f);   //GetAxis��0����1�œ��͒l���Ǘ�����A�΂߈ړ���W��A�𓯎����������
                                                                //1�ȏ�̒l�������Ă��邩��Vector3.ClampMagnitude���\�b�h���g���ē��͒l���P�ɐ�������(����)
        velocity = clampedInput * walkSpeed;
        transform.LookAt(m_Rigidbody.position + input); //�L�����N�^�[�̌��������ݒn�{���͒l�̕��Ɍ�����
                                                        //�@�����͂���v�Z�������x���猻�݂�Rigidbody�̑��x������
        velocity = velocity - m_Rigidbody.velocity;
        //�@���x��XZ��-walkSpeed��walkSpeed���Ɏ��߂čĐݒ�
        velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));

        if (isGrouded)
        {
            if (clampedInput.magnitude > 0f)
            {
               //�ړ����[�V�����ւ̑J��
            }
            else
            {
                //idle���[�V�����ւ̑J��
            }
        }
        m_Rigidbody.AddForce(m_Rigidbody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);
        // F�E�E�E��  
        // m�E�E�E����  
        // ��t�E�E�E�͂����������� (Time.fixedDeltatime) 
        //���x = F / �� * ��t
    }

    private void CheckGround()
    {
        //������@�͂ł��Ȃ�����Trigger�Ŕ��肵����

        //�W�����v�A�j���[�V�����ɑJ�ڑ���
    }
}
