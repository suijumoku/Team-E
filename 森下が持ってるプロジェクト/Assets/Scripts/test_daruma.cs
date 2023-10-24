using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_daruma : MonoBehaviour
{
    [SerializeField]
    private Transform m_shootPoint = null;
    [SerializeField]
    private Transform m_target = null;
    [SerializeField]
    private GameObject m_shootObject = null;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_target != null)
        {
            Shoot(m_target.position);
        }
    }

    private void Shoot(Vector3 i_targetPosition)
    {
        // �Ƃ肠�����K����60�x�ł�����΂��Ƃ����I
        ShootFixedAngle(i_targetPosition, 60.0f);
    }

    private void ShootFixedAngle(Vector3 i_targetPosition, float i_angle)
    {
        float speedVec = ComputeVectorFromAngle(i_targetPosition, i_angle);
        if (speedVec <= 0.0f)
        {
            // ���̈ʒu�ɒ��n�����邱�Ƃ͕s�\�̂悤���I
            Debug.LogWarning("!!");
            return;
        }

        Vector3 vec = ConvertVectorToVector3(speedVec, i_angle, i_targetPosition);
        InstantiateShootObject(vec);
    }

    private float ComputeVectorFromAngle(Vector3 i_targetPosition, float i_angle)
    {
        // xz���ʂ̋������v�Z�B
        Vector2 startPos = new Vector2(m_shootPoint.transform.position.x, m_shootPoint.transform.position.z);
        Vector2 targetPos = new Vector2(i_targetPosition.x, i_targetPosition.z);
        float distance = Vector2.Distance(targetPos, startPos);

        float x = distance;
        float g = Physics.gravity.y;
        float y0 = m_shootPoint.transform.position.y;
        float y = i_targetPosition.y;

        // Mathf.Cos()�AMathf.Tan()�ɓn���l�̒P�ʂ̓��W�A�����B�p�x�̂܂ܓn���Ă͂����Ȃ����I
        float rad = i_angle * Mathf.Deg2Rad;

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

    private Vector3 ConvertVectorToVector3(float i_v0, float i_angle, Vector3 i_targetPosition)
    {
        Vector3 startPos = m_shootPoint.transform.position;
        Vector3 targetPos = i_targetPosition;
        startPos.y = 0.0f;
        targetPos.y = 0.0f;


        Vector3 dir = (targetPos - startPos).normalized;
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 vec = i_v0 * Vector3.right;

        vec = yawRot * Quaternion.AngleAxis(i_angle, Vector3.forward) * vec;

        print("x:" + vec.x);
        print("y:" + vec.y);
        print("z:" + vec.z);

        return vec;
    }

    private void InstantiateShootObject(Vector3 i_shootVector)
    {
        if (m_shootObject == null)
        {
            throw new System.NullReferenceException("m_shootObject");
        }

        if (m_shootPoint == null)
        {
            throw new System.NullReferenceException("m_shootPoint");
        }

        var obj = Instantiate<GameObject>(m_shootObject, m_shootPoint.position, Quaternion.identity);
        var rigidbody = obj.AddComponent<Rigidbody>();

        // �����x�N�g���̂܂�AddForce()��n���Ă͂����Ȃ����B��(�����~�d��)�ɕϊ������
        Vector3 force = i_shootVector * rigidbody.mass;

        rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
