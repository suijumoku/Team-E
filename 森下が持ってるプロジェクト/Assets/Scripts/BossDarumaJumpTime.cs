using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDarumaJumpTime : MonoBehaviour
{
    const string Groundtag = "Ground";

    [Header("�v���C���[")][SerializeField] Transform P;

    [Header("�Ռ��g")][SerializeField] GameObject ShockWave;
    [Header("�Ռ��g�̔����ʒu")][SerializeField] Transform SW;
    [Header("��Ԏ���")][SerializeField] float JumpTime = 2.0f;

    [Header("�W�����vSE")][SerializeField] AudioClip JumpingSE;
    [Header("���nSE")][SerializeField] AudioClip LandingSE;

    private bool EnterGround = false;
    private bool OnGround = false;
    private bool ExitGround = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && P != null)
        {
            Jump(P.position, JumpTime);
            GameManager.instance.PlaySE(JumpingSE);
        }

        if (EnterGround)
        {
            CreateShockWave(ShockWave, SW.position);
            GameManager.instance.PlaySE(LandingSE);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Groundtag))
        {
            EnterGround = true;
            OnGround = true;
            ExitGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(Groundtag))
        {
            EnterGround = false;
            OnGround = true;
            ExitGround = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(Groundtag))
        {
            EnterGround = false;
            OnGround = false;
            ExitGround = true;
        }
    }


    void CreateShockWave(GameObject ShockWave, Vector3 ShockWavePosition)
    {
        EnterGround = false;
        Instantiate(ShockWave, ShockWavePosition, Quaternion.identity);
    }

    void Jump(Vector3 PlayerPosition, float angle)
    {
        Vector3 pos = P.transform.position;
        JumpFixedTime(PlayerPosition, angle);

    }

    private void JumpFixedTime(Vector3 PlayerPosition, float time)
    {
        float speedVec = ComputeVectorFromTime(PlayerPosition, time);
        float angle = ComputeAngleFromTime(PlayerPosition, time);

        if (speedVec <= 0.0f)
        {
            // ���̈ʒu�ɒ��n�����邱�Ƃ͕s�\�̂悤���I
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
        // �u�Ԉړ��͂�����Ɓc�c�B
        if (time <= 0.0f)
        {
            return Vector2.zero;
        }


        // xz���ʂ̋������v�Z�B
        Vector2 startPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 PlayerPos = new Vector2(PlayerPosition.x, PlayerPosition.z);
        float distance = Vector2.Distance(PlayerPos, startPos);

        float x = distance;
        // �ȁA�Ȃ��d�͂𔽓]���˂΂Ȃ�Ȃ��̂�...
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
