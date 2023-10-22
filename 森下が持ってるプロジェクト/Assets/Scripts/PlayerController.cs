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

    [SerializeField] private LayerMask groundLayers;    //地面の上なら歩きモーション、違うなら落下モーション    
    [SerializeField] private float walkSpeed = 4f;  //移動スピード
    [SerializeField] private bool isGrouded;        //接地しているかどうか

    ////　接地確認のコライダの位置のオフセット
    //[SerializeField]
    //private Vector3 groundPositionOffset = new Vector3(0f, 0.02f, 0f);
    ////　接地確認の球のコライダの半径
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
        //移動速度の計算

        var clampedInput = Vector3.ClampMagnitude(input, 1f);   //GetAxisは0から1で入力値を管理する、斜め移動でWとAを同時押しすると
                                                                //1以上の値が入ってくるからVector3.ClampMagnitudeメソッドを使って入力値を１に制限する(多分)
        velocity = clampedInput * walkSpeed;
        transform.LookAt(m_Rigidbody.position + input); //キャラクターの向きを現在地＋入力値の方に向ける
                                                        //　今入力から計算した速度から現在のRigidbodyの速度を引く
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
        // Δt・・・力を加えた時間 (Time.fixedDeltatime) 
        //速度 = F / ｍ * Δt
    }

    private void CheckGround()
    {
        //難しい方法はできないからTriggerで判定したい

        //ジャンプアニメーションに遷移多分
    }
}
