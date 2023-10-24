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

    //地面の上なら歩きモーション、違うなら落下モーション    
    [SerializeField] private float walkSpeed = 4f;  //移動スピード
    [SerializeField] private bool isGrouded;        //接地しているかどうか
    [SerializeField] GameObject CinemachineCameraTarget;

    public Vector2 look;
    public bool cursorInputForLook = true;

    float inputHorizontal;
    float inputVertical; 

    //キーボード入力かマウス入力か判定したい

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
    }
    private void FixedUpdate()
    {      

        //input = new Vector3(inputHorizontal, 0, inputVertical);

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
        //移動速度の計算
        //clampは値の範囲制限
        var clampedInput = Vector3.ClampMagnitude(moveForward, 1f);   //GetAxisは0から1で入力値を管理する、斜め移動でWとAを同時押しすると
                                                                //1以上の値が入ってくるからVector3.ClampMagnitudeメソッドを使って入力値を１に制限する(多分)

        velocity = clampedInput * walkSpeed;    //ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足すかも
                                                // transform.LookAt(m_Rigidbody.position + input); //キャラクターの向きを現在地＋入力値の方に向ける

        //Rigidbodyに一度力を加えると抵抗する力がない限りずっと力が加わる
        //AddForceに加える力をwalkSpeedで設定した速さ以上にはならないように
        //今入力から計算した速度から現在のRigidbodyの速度を引く
        velocity = velocity - m_Rigidbody.velocity;

        //　速度のXZを-walkSpeedとwalkSpeed内に収めて再設定
        velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));

        if (isGrouded)
        {
            if (clampedInput.magnitude > 0f)
            {
               //移動モーションへの遷移
            }
            else
            {
                //idleモーションへの遷移
            }
        }
        
        m_Rigidbody.AddForce(m_Rigidbody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);
        // F・・・力  
        // m・・・質量  
        // a・・・加速度
        // Δt・・・力を加えた時間 (Time.fixedDeltatime) 
        //F = ｍ * a / Δt    Forceは力を加えた時間を使って計算
    } 

    private void OnCollisionEnter(Collision collision)
    {       
        //難しい方法はできないからTriggerで判定したい
        if (collision.gameObject.tag == "Ground")
        {
            isGrouded = true;
            Debug.Log("isGrounded = " + isGrouded);
        }               
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrouded = false;
            Debug.Log("isGrounded = " + isGrouded);
        }
           
        //ジャンプアニメーションに遷移多分
    }
}
