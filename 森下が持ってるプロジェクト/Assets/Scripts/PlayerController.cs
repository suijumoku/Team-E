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
    private Vector3 velocity;

    Animator animator;
    AnimatorStateInfo stateInfo;    

    //�n�ʂ̏�Ȃ�������[�V�����A�Ⴄ�Ȃ痎�����[�V����              

     private bool isJump = false;           //�W�����v�����ǂ���
     private bool isFall = false;
     public bool isAttack = false;
     public bool isHit = false;
     private bool canMove = true;
     private bool onlyFirst = false;

    [Header("�ړ��X�s�[�h")]
    [SerializeField] private float walkSpeed = 4f;
    [Header("�������x�̒����@-����")]
    [SerializeField] float gravityPower = default!;
    [Header("�U�����[�V�����̒���")]
    [SerializeField] float Attack_Finish_Time = 0.3f;    //�U�����[�V�����̒����ɉ����ĕς��� (�A�ł�Trigger�ɂ��A�����͂�h������)
    [Header("���肪���݂��鎞��")]
    [SerializeField] float Collider_True_Time = 0.3f;
    [Header("�g���K�[�̔����^�C�~���O")]
    [SerializeField] float triggerTiming = 0.5f;         //�g���K�[���ǂ��܂ŉ������܂ꂽ�甽�����邩 �v����
    [Header("��]����")]
    [SerializeField] float smoothTime = 0.3f;                //�i�s�����ւ̉�]�ɂ����鎞��
    [Header("�W�����v�̋���")]
    [SerializeField] private float jumpPower = 5f;             //�W�����v�̂悳

    [SerializeField] Collider boxCollider = default!;
    [SerializeField] GameObject CinemachineCameraTarget;    //�J�����̃^�[�Q�b�g��ʃI�u�W�F�N�g�ɂ��邱�Ƃœ��̕�����ǔ�

    [SerializeField]  AudioClip jumpS = default!;
    [SerializeField]  AudioClip attack_true_S = default!;
    [SerializeField]  AudioClip fallS = default!;

    [SerializeField] MainGameManager _MainGameManager;

    [SerializeField]�@ResultManager _ResultManager; //�f�o�b�O�p


    float inputHorizontal;      //���������̓��͒l
    float inputVertical;        //���������̓��͒l
    float inputTrigger;
    bool  inputAttack;

    bool  isReset;  
    float targetRotation;
    float yVelocity = 0.0f;
    float motionTime = 0.0f;             // �U�����[�V�������n�܂��Ă���̌o�ߎ��Ԃ��i�[->animation�̑J�ڂłł����� 
  
    float time = 0f;                //Running���[�V�����Ɏg��


    //private PlayerController instance;

    //public void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}


    void Start()
    {             
        m_Rigidbody = GetComponent<Rigidbody>();     
        animator = GetComponent<Animator>();
        _MainGameManager.isInvincible = false;
    }

    void Update()
    {
        
         
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        inputHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal");   //���͒l�̊i�[
        inputVertical = UnityEngine.Input.GetAxisRaw("Vertical");
        inputTrigger = UnityEngine.Input.GetAxis("L_R_Trigger");
        inputAttack = UnityEngine.Input.GetButtonDown("Attack");

        Jump();
        Attack();
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            //���͂�0����Ȃ���Έړ����[�V�����ɑJ��
            time += Time.deltaTime;
        }
        else
        {
            time = 0f;
        }
        animator.SetFloat("time", time);


        if (isAttack)
        {
            motionTime += Time.deltaTime;

            if (isHit && onlyFirst == false)
            {
                boxCollider.enabled = false;
                onlyFirst = true;
            }
           // checkHit();
            if (motionTime >= Attack_Finish_Time)
            {                           
                isHit = false;
                isAttack = false;
                onlyFirst = false;
            }                    
            if (motionTime >= Collider_True_Time)
            {
                boxCollider.enabled = false;
            }
        }       

         
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

        if (other.gameObject.CompareTag("Enemy") &&
            _MainGameManager.isInvincible == false)     //���G���Ԓ��̓_���[�W��H���Ȃ�
        {
            _MainGameManager.Miss();
        }

    }


    void checkHit()
    {
        //�U�����Ƃ������������ǂ������肵�����B�ꍇ�ɂ���Ă̓X�N���v�g�����Ă��悳�����B
    }

    void Attack()   //�W�����v���͍U���ł��Ȃ�
    {

        if (inputTrigger == 0 && inputAttack == false)
        {
            isReset = true;
            return;
        }
        if (isAttack == true || isJump == true || isReset == false) return;
       

        if (inputTrigger > triggerTiming || inputAttack)  //A�{�^����RT�ōU��
        {
            animator.SetTrigger("toAttacking");
            GameManager.instance.PlaySE(attack_true_S);         //�� �����������ǂ����ŉ��ς���Ǝv����
            isAttack = true;
            isReset = false;
            boxCollider.enabled = true;
            motionTime = 0.0f;
            // Debug.Log("isAttack = " + isAttack);
            //�U�����[�V�����ւ̑J��
            //_ResultManager.NormalHit(); //�f�o�b�O�p
        }

    }

    public void fall()
    {
   
        GameManager.instance.PlaySE(fallS);
        isFall = true;    
        canMove = false;
      
        //�����ő���s�\�ɂ���΂��ꂷ�ꂩ�畜�A�������ɃW�����v���ł��Ȃ��Ȃ邱�Ƃ�h������
        //�������[�V�����ւ̑J��
    }

    void Move()
    {
        if (!canMove) //�U�����͈ړ����W�����v���ł��Ȃ�->return����Ȃ��Ă��̏�ŌŒ肳������
            return;
        //else if (Mathf.Approximately(inputHorizontal, 0.0f) && Mathf.Approximately(inputVertical, 0.0f) && isJump == true)    
        //{
        //    //inputHorizontal += 0.1f;  ���͂͐����̒l�����炱�ꂾ�ƃ_��    beforePos��nowPos���g���΂ł������H
        //    //inputVertical   += 0.1f;
        //    return;     //�W�����v���ɓ��͒l���قڃ[���Ȃ�return����Ύ��R�Ȋ������������� -> �኱�s���R�ȓ����ɁB�v���P
        //}

        if (isAttack == true)
        {
            inputHorizontal = 0;
            inputVertical = 0;
        }

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
        if (isJump == true || isFall == true || isAttack == true) return;   //�������ƍU�����̓W�����v�������Ȃ�

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
