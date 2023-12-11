using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDarumaJumpAngle : MonoBehaviour
{
    [Header("�v���C���[")][SerializeField] Transform P;
    [Header("��Ԋp�x")][SerializeField] float JumpAngle=80.0f;

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
            // ���̈ʒu�ɒ��n�����邱�Ƃ͕s�\�̂悤���I
            Debug.LogWarning("!!");
            return;
        }

        Vector3 vec = ConvertVectorToVector3(speedVec, angle, PlayerPosition);
        DoJumpAttack(vec);
    }

    private float ComputeVectorFromAngle(Vector3 PlayerPosition, float angle)
    {
        // xz���ʂ̋������v�Z�B
        Vector2 startPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 PlayerPos = new Vector2(PlayerPosition.x, PlayerPosition.z);
        float distance = Vector2.Distance(PlayerPos, startPos);

        float x = distance;
        float g = Physics.gravity.y;
        float y0 = this.transform.position.y;
        float y = PlayerPosition.y;

        // Mathf.Cos()�AMathf.Tan()�ɓn���l�̒P�ʂ̓��W�A�����B�p�x�̂܂ܓn���Ă͂����Ȃ����I
        float rad = angle * Mathf.Deg2Rad;

        float cos = Mathf.Cos(rad);
        float tan = Mathf.Tan(rad);

        float v0Square = g * x * x / (2 * cos * cos * (y - y0 - x * tan));

        // �����𕽕����v�Z����Ƌ����ɂȂ��Ă��܂��B
        // ������float�ł͕\���ł��Ȃ��B
        // ���������ꍇ�͂���ȏ�̌v�Z�͑ł��؂낤�B
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
