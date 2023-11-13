using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpownenemyScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��������GameObject")]
    private GameObject createPrefab;
    [SerializeField]
    [Tooltip("��������͈�A")]
    private Transform rangeA;
    [SerializeField]
    [Tooltip("��������͈�B")]
    private Transform rangeB;

    int a;

    // �o�ߎ���
    private float time = 0f;

    public void Hit()
    {
        a = a - 1;
    }

    // Update is called once per frame
    void Update()
    {
        // �O�t���[������̎��Ԃ����Z���Ă���

        // ��1�b�u���Ƀ����_���ɐ��������悤�ɂ���B
        if (time == 0f || time > 10f)
        {
            for (int i = 0; i < 2; i++)
            {
                a++;
                // rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
                float x = Random.Range(rangeA.position.x, rangeB.position.x);
                // rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
                float y = Random.Range(rangeA.position.y, rangeB.position.y);
                // rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
                float z = Random.Range(rangeA.position.z, rangeB.position.z);

                if (a < 11)
                {
                    // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
                    Instantiate(createPrefab, new Vector3(x, y, z), createPrefab.transform.rotation);
                }

            }

            time = 0f;
        }
        time = time + Time.deltaTime;
        //Debug.Log(time);
    }
}