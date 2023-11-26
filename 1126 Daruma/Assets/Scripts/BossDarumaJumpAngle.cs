using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDarumaJumpAngle : MonoBehaviour
{
    [Header("プレイヤー")][SerializeField] Transform P;
    [Header("飛ぶ角度")][SerializeField] float JumpAngle=80.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && P != null)
        {
            Jump(P.position,JumpAngle);
        }
    }

    void Jump(Vector3 PlayerPosition,float angle)
    {
        Vector3 pos = P.transform.position;
        JumpFixedAngle(PlayerPosition, angle);

    }

    private void JumpFixedAngle(Vector3 PlayerPosition, float angle)
    {
        float speedVec = ComputeVectorFromAngle(PlayerPosition, angle);
        if (speedVec <= 0.0f)
        {
            // その位置に着地させることは不可能のようだ！
            Debug.LogWarning("!!");
            return;
        }

        Vector3 vec = ConvertVectorToVector3(speedVec, angle, PlayerPosition);
        DoJumpAttack(vec);
    }

    private float ComputeVectorFromAngle(Vector3 PlayerPosition, float angle)
    {
        // xz平面の距離を計算。
        Vector2 startPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 PlayerPos = new Vector2(PlayerPosition.x, PlayerPosition.z);
        float distance = Vector2.Distance(PlayerPos, startPos);

        float x = distance;
        float g = Physics.gravity.y;
        float y0 = this.transform.position.y;
        float y = PlayerPosition.y;

        // Mathf.Cos()、Mathf.Tan()に渡す値の単位はラジアンだ。角度のまま渡してはいけないぞ！
        float rad = angle * Mathf.Deg2Rad;

        float cos = Mathf.Cos(rad);
        float tan = Mathf.Tan(rad);

        float v0Square = g * x * x / (2 * cos * cos * (y - y0 - x * tan));

        // 負数を平方根計算すると虚数になってしまう。
        // 虚数はfloatでは表現できない。
        // こういう場合はこれ以上の計算は打ち切ろう。
        if (v0Square <= 0.0f)
        {
            return 0.0f;
        }

        float v0 = Mathf.Sqrt(v0Square);
        return v0;
    }

    private Vector3 ConvertVectorToVector3(float i_v0, float i_angle, Vector3 PlayerPosition)
    {
        Vector3 startPos = this.transform.position;
        Vector3 PlayerPos = PlayerPosition;
        startPos.y = 0.0f;
        PlayerPos.y = 0.0f;


        Vector3 dir = (PlayerPos - startPos).normalized;
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 vec = i_v0 * Vector3.right;

        vec = yawRot * Quaternion.AngleAxis(i_angle, Vector3.forward) * vec;

        print("x:" + vec.x);
        print("y:" + vec.y);
        print("z:" + vec.z);

        return vec;
    }

    private void DoJumpAttack(Vector3 JumpVector)
    {
        var rigidbody = GetComponent<Rigidbody>();

        Vector3 force = JumpVector * rigidbody.mass;

        rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
