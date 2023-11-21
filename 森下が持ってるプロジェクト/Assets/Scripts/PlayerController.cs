//古澤
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

    //地面の上なら歩きモーション、違うなら落下モーション              

     private bool isJump = false;           //ジャンプ中かどうか
     private bool isFall = false;
     public bool isAttack = false;
     public bool isHit = false;
     private bool canMove = true;
     private bool onlyFirst = false;

    [Header("移動スピード")]
    [SerializeField] private float walkSpeed = 4f;
    [Header("落下速度の調整　-つける")]
    [SerializeField] float gravityPower = default!;
    [Header("攻撃モーションの長さ")]
    [SerializeField] float Attack_Finish_Time = 1.5f;    //攻撃モーションの長さに応じて変える (連打とTriggerによる連続入力を防ぐため)
    [Header("トリガーの反応タイミング")]
    [SerializeField] float triggerTiming = 0.5f;         //トリガーがどこまで押し込まれたら反応するか 要調整
    [Header("回転時間")]
    [SerializeField] float smoothTime = 0.3f;                //進行方向への回転にかかる時間
    [Header("ジャンプの強さ")]
    [SerializeField] private float jumpPower = 5f;             //ジャンプのつよさ

    [SerializeField] Collider boxCollider = default!;
    [SerializeField] GameObject CinemachineCameraTarget;    //カメラのターゲットを別オブジェクトにすることで頭の部分を追尾

    [SerializeField]  AudioClip jumpS = default!;
    [SerializeField]  AudioClip attack_true_S = default!;
    [SerializeField]  AudioClip fallS = default!;

    [SerializeField] MainGameManager _MainGameManager;

    [SerializeField]　ResultManager _ResultManager; //デバッグ用


    float inputHorizontal;      //水平方向の入力値
    float inputVertical;        //垂直方向の入力値
    float targetRotation;
    float yVelocity = 0.0f;
    float motionTime = 0.0f;             // 攻撃モーションが始まってからの経過時間を格納->animationの遷移でできそう 
  
    float time = 0f;                //Runningモーションに使う


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

        inputHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal");   //入力値の格納
        inputVertical = UnityEngine.Input.GetAxisRaw("Vertical");


        Jump();
        Attack();
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            //入力が0じゃなければ移動モーションに遷移
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
                boxCollider.enabled = false;
                isAttack = false;
                isHit = false;
                onlyFirst = false;
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
        //難しい方法はできないからTriggerで判定したい
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
            _MainGameManager.isInvincible == false)     //無敵時間中はダメージを食らわない
        {
            _MainGameManager.Miss();
        }

    }


    void checkHit()
    {
        //振った槌が当たったかどうか判定したい。場合によってはスクリプト分けてもよさそう。
    }

    void Attack()   //ジャンプ中は攻撃できない
    {
        if (isAttack == true || isJump == true) return;

        if (UnityEngine.Input.GetAxis("L_R_Trigger") > triggerTiming || UnityEngine.Input.GetButtonDown("Attack"))  //AボタンかRTで攻撃
        {
            animator.SetTrigger("toAttacking");
            GameManager.instance.PlaySE(attack_true_S);         //仮 当たったかどうかで音変えると思われる
            isAttack = true;
            boxCollider.enabled = true;
            motionTime = 0.0f;
           // Debug.Log("isAttack = " + isAttack);
            //攻撃モーションへの遷移
            //_ResultManager.NormalHit(); //デバッグ用
        }
     
    }

    public void fall()
    {
   
        GameManager.instance.PlaySE(fallS);
        isFall = true;    
        canMove = false;
      
        //ここで操作不能にすればすれすれから復帰した時にジャンプができなくなることを防げそう
        //落下モーションへの遷移
    }

    void Move()
    {
        if (!canMove) //攻撃中は移動もジャンプもできない->returnじゃなくてその場で固定させたい
            return;
        //else if (Mathf.Approximately(inputHorizontal, 0.0f) && Mathf.Approximately(inputVertical, 0.0f) && isJump == true)    
        //{
        //    //inputHorizontal += 0.1f;  入力は正負の値だからこれだとダメ    beforePosとnowPosを使えばできそう？
        //    //inputVertical   += 0.1f;
        //    return;     //ジャンプ中に入力値がほぼゼロならreturnすれば自然な慣性が働きそう -> 若干不自然な動きに。要改善
        //}

        if (isAttack == true)
        {
            inputHorizontal = 0;
            inputVertical = 0;
        }

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;    

     
        //移動速度の計算
        //clampは値の範囲制限
        var clampedInput = Vector3.ClampMagnitude(moveForward, 1f);   //GetAxisは0から1で入力値を管理する、斜め移動でWとAを同時押しすると
                                                                      //1以上の値が入ってくるからVector3.ClampMagnitudeメソッドを使って入力値を１に制限する(多分)
    
         velocity = clampedInput * walkSpeed;
        // transform.LookAt(m_Rigidbody.position + input); //キャラクターの向きを現在地＋入力値の方に向ける

        //Rigidbodyに一度力を加えると抵抗する力がない限りずっと力が加わる
        //AddForceに加える力をwalkSpeedで設定した速さ以上にはならないように
        //今入力から計算した速度から現在のRigidbodyの速度を引く
        velocity = velocity - m_Rigidbody.velocity;

        //　速度のXZを-walkSpeedとwalkSpeed内に収めて再設定
        velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));

        if (moveForward != Vector3.zero)
        {
            //SmoothDampAngleで滑らかな回転をするためには引数（moveForwardとvelocityだけ）をVector3からfloatに変換しなければいけない

            targetRotation = Mathf.Atan2(moveForward.x, moveForward.z) * Mathf.Rad2Deg;     //Atan2, ベクトルを角度(ラジアン)に変換する Rad2Deg(radian to degrees?)ラジアンから度に変換する

            //SmoothDampAngle(現在の値, 目的の値, ref 現在の速度, 遷移時間, 最高速度); 現在の速度はnullで良いっぽい？
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref yVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        m_Rigidbody.AddForce(m_Rigidbody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);
        // F・・・力  
        // m・・・質量  
        // a・・・加速度
        // Δt・・・力を加えた時間 (Time.fixedDeltatime) 
        //F = ｍ * a / Δt    Forceは力を加えた時間を使って計算
    }

    void Jump()
    {
        if (isJump == true || isFall == true || isAttack == true) return;   //落下中と攻撃中はジャンプをさせない

        if ( UnityEngine.Input.GetButtonDown("Jump"))
        {         
            GameManager.instance.PlaySE(jumpS);
            m_Rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            //Debug.Log("isjump = " + isJump);

            //ジャンプモーション→落下モーションに遷移
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
