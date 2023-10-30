using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    private Vector3 velocity;
    private Vector3 input;

    //Animator animator;
    //AnimatorStateInfo stateInfo;

    //private float _cinemachineTargetYaw;
    //private float _cinemachineTargetPitch;

    //�n�ʂ̏�Ȃ�������[�V�����A�Ⴄ�Ȃ痎�����[�V����    
          
     private bool pushJumpButton;   //�W�����v�{�^�������������ǂ���
     private bool isJump = false;           //�W�����v�����ǂ���

    [SerializeField] private float walkSpeed = 4f;  //�ړ��X�s�[�h   
    [SerializeField] float gravityPower;            //�������x�̒����@-����
    [SerializeField] private float jumpPower = 5f; //�W�����v�̂悳
    [SerializeField] GameObject CinemachineCameraTarget;

    public Vector2 look;
    public bool cursorInputForLook = true;

    float inputHorizontal;
    float inputVertical; 

    //�L�[�{�[�h���͂��}�E�X���͂����肵����

    void Start()
    {
       // _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        m_Rigidbody = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        inputHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        inputVertical = UnityEngine.Input.GetAxisRaw("Vertical");
        Jump();
    }
    private void FixedUpdate()
    {
        Gravity();

        //input = new Vector3(inputHorizontal, 0, inputVertical);

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
        //�ړ����x�̌v�Z
        //clamp�͒l�͈̔͐���
        var clampedInput = Vector3.ClampMagnitude(moveForward, 1f);   //GetAxis��0����1�œ��͒l���Ǘ�����A�΂߈ړ���W��A�𓯎����������
                                                                //1�ȏ�̒l�������Ă��邩��Vector3.ClampMagnitude���\�b�h���g���ē��͒l���P�ɐ�������(����)
        
        if (pushJumpButton)
        {
            velocity = clampedInput * walkSpeed + new Vector3(0, jumpPower, 0);    //�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂�

        }
        else
          velocity = clampedInput * walkSpeed;   
                                                // transform.LookAt(m_Rigidbody.position + input); //�L�����N�^�[�̌��������ݒn�{���͒l�̕��Ɍ�����

        //Rigidbody�Ɉ�x�͂�������ƒ�R����͂��Ȃ����肸���Ɨ͂������
        //AddForce�ɉ�����͂�walkSpeed�Őݒ肵�������ȏ�ɂ͂Ȃ�Ȃ��悤��
        //�����͂���v�Z�������x���猻�݂�Rigidbody�̑��x������
        velocity = velocity - m_Rigidbody.velocity;

        //�@���x��XZ��-walkSpeed��walkSpeed���Ɏ��߂čĐݒ�
        velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));

        if (isJump == false)
        {
            //��O�̃t���[���̍����ƍ��̍������ׂĉ������Ă����痎�����[�V�����H
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
        // a�E�E�E�����x
        // ��t�E�E�E�͂����������� (Time.fixedDeltatime) 
        //F = �� * a / ��t    Force�͗͂����������Ԃ��g���Čv�Z
        
        pushJumpButton= false;
    }  

    private void OnCollisionEnter(Collision other)
    {       
        //������@�͂ł��Ȃ�����Trigger�Ŕ��肵����
        if (isJump == true)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isJump = false;
                Debug.Log("isJump = " + isJump);
            }
        }                    
    }

    void Jump()
    {
        if (isJump == true) return;
        if (UnityEngine.Input.GetKeyDown (KeyCode.Space) || UnityEngine.Input.GetButtonDown("Jump"))
        {          
            m_Rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            Debug.Log("isjump = " + isJump);
        }
    }

    void Gravity()
    {
        if (isJump == true)
        {
            m_Rigidbody.AddForce(new Vector3(0, gravityPower, 0));
        }
    }
}
