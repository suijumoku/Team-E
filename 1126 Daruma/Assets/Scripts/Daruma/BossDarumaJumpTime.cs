using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDarumaJumpTime : MonoBehaviour
{
    const string Groundtag = "Ground";

    [Header("プレイヤー")][SerializeField] Transform P;

    [Header("衝撃波")][SerializeField] GameObject ShockWave;
    [Header("衝撃波の発生位置")][SerializeField] Transform SW;
    [Header("飛ぶ時間")][SerializeField] float JumpTime = 2.0f;
    [Header("飛ぶ距離の制限")][SerializeField] float JumpLength = 2.0f;

    [Header("ジャンプSE")][SerializeField] AudioClip JumpingSE;
    [Header("着地SE")][SerializeField] AudioClip LandingSE;

    private bool EnterGround = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && P != null)
        {
            // ジャンプ
            Jump(P.position, JumpTime);
            GameManager.instance.PlaySE(JumpingSE);
        }

        if (EnterGround)
        {
            // 地面についたら衝撃波を出す
            CreateShockWave(ShockWave, SW.position);
            GameManager.instance.PlaySE(LandingSE);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Groundtag))
        {
            EnterGround = true;

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(Groundtag))
        {
            EnterGround = false;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(Groundtag))
        {
            EnterGround = false;

        }
    }


    void CreateShockWave(GameObject ShockWave, Vector3 ShockWavePosition)
    {
        // 衝撃波を生成
        EnterGround = false;
        Instantiate(ShockWave, ShockWavePosition, Quaternion.identity);
    }

    void Jump(Vector3 PlayerPosition, float angle)
    {
        JumpFixedTime(PlayerPosition, angle);
    }

    private void JumpFixedTime(Vector3 PlayerPosition, float time)
    {
        float speedVec = ComputeVectorFromTime(PlayerPosition, time);
        float angle = ComputeAngleFromTime(PlayerPosition, time);

        if (speedVec <= 0.0f)
        {
            // 着地不可能の場合はログを出す
            Debug.LogWarning("!!");
            return;
        }

        Vector3 vec = ConvertVectorToVector3(speedVec, angle, PlayerPosition);
        DoJumpAttack(vec);
    }

    private float ComputeVectorFromTime(Vector3 PlayerPosition, float time)
    {
        Vector2 vec = ComputeVectorXYFromTime(PlayerPosition, time);

        float v_x = vec.x;
        float v_y = vec.y;

        float v0Square = v_x * v_x + v_y * v_y;
       
        // 虚数になるため計算打ち切り
        if (v0Square <= 0.0f)
        {
            return 0.0f;
        }

        float v0 = Mathf.Sqrt(v0Square);

        return v0;
    }

    private float ComputeAngleFromTime(Vector3 PlayerPosition, float time)
    {
        Vector2 vec = ComputeVectorXYFromTime(PlayerPosition, time);

        float v_x = vec.x;
        float v_y = vec.y;

        float rad = Mathf.Atan2(v_y, v_x);
        float angle = rad * Mathf.Rad2Deg;

        return angle;
    }

    private Vector2 ComputeVectorXYFromTime(Vector3 PlayerPosition, float time)
    {
        // 瞬間移動は禁止
        if (time <= 0.0f)
        {
            return Vector2.zero;
        }

        // xz平面の距離を計算する
        Vector2 startPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 PlayerPos = new Vector2(PlayerPosition.x, PlayerPosition.z);
        float distance = Vector2.Distance(PlayerPos, startPos);

        // ジャンプ距離制限以上に離れている場合は飛ぶ距離を制限まで下げる
        if (distance>JumpLength)
        {
            distance = JumpLength;
        }

        float x = distance;
        float g = -Physics.gravity.y;
        float y0 = this.transform.position.y;
        float y = PlayerPosition.y;
        float t = time;

        float v_x = x / t;
        float v_y = (y - y0) / t + (g * t) / 2;

        return new Vector2(v_x, v_y);
    }

    private Vector3 ConvertVectorToVector3(float v0, float angle, Vector3 PlayerPosition)
    {
        Vector3 startPos = this.transform.position;
        Vector3 PlayerPos = PlayerPosition;
        startPos.y = 0.0f;
        PlayerPos.y = 0.0f;


        Vector3 dir = (PlayerPos - startPos).normalized;
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 vec = v0 * Vector3.right;

        vec = yawRot * Quaternion.AngleAxis(angle, Vector3.forward) * vec;


        return vec;
    }

    private void DoJumpAttack(Vector3 JumpVector)
    {
        var rigidbody = GetComponent<Rigidbody>();

        Vector3 force = JumpVector * rigidbody.mass;

        rigidbody.AddForce(force, ForceMode.Impulse);

    }
}
