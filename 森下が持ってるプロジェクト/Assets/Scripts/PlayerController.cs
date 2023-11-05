//���V
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Vector3 nowPos, beforePos;
    private Vector3 velocity;
    private Vector3 input;

    //Animator animator;
    //AnimatorStateInfo stateInfo;    

    //�n�ʂ̏�Ȃ�������[�V�����A�Ⴄ�Ȃ痎�����[�V����              

     private bool isJump = false;           //�W�����v�����ǂ���
     private bool isFall = false;
     private bool isAttack = false;
     private bool canMove = true;

    [SerializeField] private float walkSpeed = 4f;             //�ړ��X�s�[�h   
    [SerializeField] float gravityPower = default!;            //�������x�̒����@-����
    [SerializeField] float Attack_Finish_Time = 1.5f;    //�U�����[�V�����̒����ɉ����ĕς��� (�A�ł�Trigger�ɂ��A�����͂�h������)
    [SerializeField] float triggerTiming = 0.5f;         //�g���K�[���ǂ��܂ŉ������܂ꂽ�甽�����邩 �v����
    [SerializeField] float smoothTime = 0.3f;                //�i�s�����ւ̉�]�ɂ����鎞��
    [SerializeField] private float jumpPower = 5f;             //�W�����v�̂悳

    [SerializeField] Miss miss;
    [SerializeField] GameObject CinemachineCameraTarget;    //�J�����̃^�[�Q�b�g��ʃI�u�W�F�N�g�ɂ��邱�Ƃœ��̕�����ǔ�

    [SerializeField]  AudioClip jumpS = default!;
    [SerializeField]  AudioClip attack_true_S = default!;
    [SerializeField] AudioClip fallS = default!;
    //[SerializeField] private AudioClip attack_false_S = default!;

    //public Vector2 look;
    //public bool cursorInputForLook = true;

    //private PlayerController instance;

    float inputHorizontal;      //���������̓��͒l
    float inputVertical;        //���������̓��͒l
    float targetRotation;
    float yVelocity = 0.0f;
    private float motionTime = 0.0f;             // �U�����[�V�������n�܂��Ă���̌o�ߎ��Ԃ��i�[   

    //public void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}
    void Start()
    {
        //jumpS = GetComponent<AudioClip>();
        //attack_true_S = GetComponent<AudioClip>();
        //attack_false_S GetComponent<AudioClip>();

        m_Rigidbody = GetComponent<Rigidbody>();
        beforePos = GetComponent<Transform>().position;
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        inputHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal");   //���͒l�̊i�[
        inputVertical = UnityEngine.Input.GetAxisRaw("Vertical");

        
        Jump();                     
        Attack();
        nowPos = GetComponent<Transform>().position;

        if (isJump == false && isFall == false)
        {
            //��O�̃t���[���̈ʒu�ƍ��̈ʒu���ׂĕω����Ă�����ړ����[�V�����ɑJ��

          
            if (nowPos.x != beforePos.x || nowPos.z != beforePos.z)
            {
                //�ړ����[�V�����ւ̑J��
          
            }
            else
            {
                //idle���[�V�����ւ̑J��
             
            }
        }

        if (isAttack)
        {
            motionTime += Time.deltaTime;
            checkHit();
            if (motionTime >= Attack_Finish_Time)
                isAttack = false;
        }       

        beforePos = nowPos;       
    }
    private void FixedUpdate()
    {       
        Gravity();

        Move();             
    }  

    private void OnCollisionEnter(Collision other)
    {       
        //������@�͂ł��Ȃ�����Trigger�Ŕ��肵����
        if (isJump == true || isFall == true)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isJump = false;
                isFall = false;
                canMove = true;
                //Debug.Log("isJump = " + isJump);
            }
        }

        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    miss.InOrder();
        //}

    }


    void checkHit()
    {
        //�U�����Ƃ������������ǂ������肵�����B�ꍇ�ɂ���Ă̓X�N���v�g�����Ă��悳�����B
    }

    void Attack()   //�W�����v���͍U���ł��Ȃ�
    {
        if (isAttack == true || isJump == true) return;

        if (UnityEngine.Input.GetAxis("L_R_Trigger") > triggerTiming || UnityEngine.Input.GetButtonDown("Attack"))  //A�{�^����RT�ōU��
        {
            GameManager.instance.PlaySE(attack_true_S);         //�� �����������ǂ����ŉ��ς���Ǝv����
            isAttack = true;
            motionTime = 0.0f;
            //Debug.Log("isAttack = " + isAttack);
            //�U�����[�V�����ւ̑J��
        }    
     
    }

    public void fall()
    {
        GameManager.instance.PlaySE(fallS);
        isFall = true;
        Debug.Log("nowPos.y = " + nowPos.y);
        Debug.Log("beforePos.y = " + beforePos.y);
        canMove = false;
        //�����ő���s�\�ɂ���΂��ꂷ�ꂩ�畜�A�������ɃW�����v���ł��Ȃ��Ȃ邱�Ƃ�h������
        //�������[�V�����ւ̑J��
    }

    void Move()
    {
        if (!canMove)
            return;
        //else if (Mathf.Approximately(inputHorizontal, 0.0f) && Mathf.Approximately(inputVertical, 0.0f) && isJump == true)    
        //{
        //    //inputHorizontal += 0.1f;  ���͂͐����̒l�����炱�ꂾ�ƃ_��    beforePos��nowPos���g���΂ł������H
        //    //inputVertical   += 0.1f;
        //    return;     //�W�����v���ɓ��͒l���قڃ[���Ȃ�return����Ύ��R�Ȋ������������� -> �኱�s���R�ȓ����ɁB�v���P
        //}

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;    

     
        //�ړ����x�̌v�Z
        //clamp�͒l�͈̔͐���
        var clampedInput = Vector3.ClampMagnitude(moveForward, 1f);   //GetAxis��0����1�œ��͒l���Ǘ�����A�΂߈ړ���W��A�𓯎����������
                                                                      //1�ȏ�̒l�������Ă��邩��Vector3.ClampMagnitude���\�b�h���g���ē��͒l���P�ɐ�������(����)
    
         velocity = clampedInput * walkSpeed;
        // transform.LookAt(m_Rigidbody.position + input); //�L�����N�^�[�̌��������ݒn�{���͒l�̕��Ɍ�����

        //Rigidbody�Ɉ�x�͂�������ƒ�R����͂��Ȃ����肸���Ɨ͂������
        //AddForce�ɉ�����͂�walkSpeed�Őݒ肵�������ȏ�ɂ͂Ȃ�Ȃ��悤��
        //�����͂���v�Z�������x���猻�݂�Rigidbody�̑��x������
        velocity = velocity - m_Rigidbody.velocity;

        //�@���x��XZ��-walkSpeed��walkSpeed���Ɏ��߂čĐݒ�
        velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));

        if (moveForward != Vector3.zero)
        {
            //SmoothDampAngle�Ŋ��炩�ȉ�]�����邽�߂ɂ͈����imoveForward��velocity�����j��Vector3����float�ɕϊ����Ȃ���΂����Ȃ�

            targetRotation = Mathf.Atan2(moveForward.x, moveForward.z) * Mathf.Rad2Deg;     //Atan2, �x�N�g�����p�x(���W�A��)�ɕϊ����� Rad2Deg(radian to degrees?)���W�A������x�ɕϊ�����

            //SmoothDampAngle(���݂̒l, �ړI�̒l, ref ���݂̑��x, �J�ڎ���, �ō����x); ���݂̑��x��null�ŗǂ����ۂ��H
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref yVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        m_Rigidbody.AddForce(m_Rigidbody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);
        // F�E�E�E��  
        // m�E�E�E����  
        // a�E�E�E�����x
        // ��t�E�E�E�͂����������� (Time.fixedDeltatime) 
        //F = �� * a / ��t    Force�͗͂����������Ԃ��g���Čv�Z
    }

    void Jump()
    {
        if (isJump == true || isFall == true) return;   //�������̓W�����v�������Ȃ�

        if ( UnityEngine.Input.GetButtonDown("Jump"))
        {
            GameManager.instance.PlaySE(jumpS);
            m_Rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            //Debug.Log("isjump = " + isJump);

            //�W�����v���[�V�������������[�V�����ɑJ��
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
