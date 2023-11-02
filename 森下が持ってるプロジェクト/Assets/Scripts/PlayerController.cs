//古澤
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //地面の上なら歩きモーション、違うなら落下モーション    
          
     private bool pushJumpButton;   //ジャンプボタンを押したかどうか
     private bool isJump = false;           //ジャンプ中かどうか
     private bool isGrounded = true;
     private bool isAttack = false;

    [SerializeField] private float walkSpeed = 4f;  //移動スピード   
    [SerializeField] float gravityPower;            //落下速度の調整　-つける
    [SerializeField] private float jumpPower = 5f; //ジャンプのつよさ

    [SerializeField] Miss miss;
    [SerializeField] GameObject CinemachineCameraTarget;    //カメラのターゲットを別オブジェクトにすることで頭の部分を追尾

    public Vector2 look;
    public bool cursorInputForLook = true;

    float inputHorizontal;
    float inputVertical; 

    //キーボード入力かマウス入力か判定したい

    void Start()
    {
      
        m_Rigidbody = GetComponent<Rigidbody>();
        beforePos = GetComponent<Transform>().position;
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        inputHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        inputVertical = UnityEngine.Input.GetAxisRaw("Vertical");
        Jump();
        nowPos = GetComponent<Transform>().position;

        if (isJump == false)
        {
            //一つ前のフレームの高さと今の高さを比べて下がっていたら落下モーションに遷移
            if (nowPos.y < beforePos.y && isGrounded == true)
            {
                isGrounded = false;
                Debug.Log("isGrounded = " + isGrounded);
                //落下モーションへの遷移
            }
            else if (nowPos.x != beforePos.x || nowPos.z != beforePos.z)
            {
                //移動モーションへの遷移
          
            }
            else
            {
                //idleモーションへの遷移
             
            }
        }
        beforePos = nowPos;
    }
    private void FixedUpdate()
    {
       
        Gravity();

        Move(); 

       
        pushJumpButton = false;
    }  

    private void OnCollisionEnter(Collision other)
    {       
        //難しい方法はできないからTriggerで判定したい
        if (isJump == true || isGrounded == false)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isJump = false;
                isGrounded = true;
                //Debug.Log("isJump = " + isJump);
            }
        }

        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    miss.InOrder();
        //}

    }

    void Attack()
    {
        if (isAttack == true) return;

        if (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetButtonDown("Attack"))
        {
            isAttack = true;
            Debug.Log("isAttack = " + isAttack);
            //攻撃モーションへの遷移
        }    

        isAttack = false;
    }

    void Move()
    {
        //if (inputHorizontal == null && inputVertical == null)


        //input = new Vector3(inputHorizontal, 0, inputVertical);

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;    

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
        //移動速度の計算
        //clampは値の範囲制限
        var clampedInput = Vector3.ClampMagnitude(moveForward, 1f);   //GetAxisは0から1で入力値を管理する、斜め移動でWとAを同時押しすると
                                                                      //1以上の値が入ってくるからVector3.ClampMagnitudeメソッドを使って入力値を１に制限する(多分)

        if (pushJumpButton)
        {
            velocity = clampedInput * walkSpeed + new Vector3(0, jumpPower, 0);    //ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す
        }
        else
            velocity = clampedInput * walkSpeed;
        // transform.LookAt(m_Rigidbody.position + input); //キャラクターの向きを現在地＋入力値の方に向ける

        //Rigidbodyに一度力を加えると抵抗する力がない限りずっと力が加わる
        //AddForceに加える力をwalkSpeedで設定した速さ以上にはならないように
        //今入力から計算した速度から現在のRigidbodyの速度を引く
        velocity = velocity - m_Rigidbody.velocity;

        //　速度のXZを-walkSpeedとwalkSpeed内に収めて再設定
        velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));


        m_Rigidbody.AddForce(m_Rigidbody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);
        // F・・・力  
        // m・・・質量  
        // a・・・加速度
        // Δt・・・力を加えた時間 (Time.fixedDeltatime) 
        //F = ｍ * a / Δt    Forceは力を加えた時間を使って計算
    }

    void Jump()
    {
        if (isJump == true) return;
        if (UnityEngine.Input.GetKeyDown (KeyCode.Space) || UnityEngine.Input.GetButtonDown("Jump"))
        {          
            m_Rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            Debug.Log("isjump = " + isJump);

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
