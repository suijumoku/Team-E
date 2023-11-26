using System.Collections.Generic;
using UnityEngine;

public class SpownenemyScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��������GameObject")]
    private GameObject createPrefab;
    [SerializeField]
    [Tooltip("��������ꏊ")]
    private Transform[] SpownPoints;
    [SerializeField]
    [Tooltip("��������Ԋu")]
    private float SpownTime = 5;


    int enemyCount = 0;

    // �o�ߎ���
    private float time = 0f;
    // ���X�|�[���ʒu���v���C���[�Ɣ���Ă��Ȃ��ꏊ���烉���_���Ɍ��߂邽�߂̃��X�g
    List<Transform> transforms = new List<Transform>();

    // Update is called once per frame
    void Update()
    {

        if (time >= SpownTime)
        {
            transforms.Clear();
            for (int i = 0; i < SpownPoints.Length; i++)
            {
                if (!SpownPoints[i].GetComponent<PlayerTriggerCheck>().isOn)
                {
                    transforms.Add(SpownPoints[i]);
                }
            }


            for (int i = 0; i < 2; i++)
            {
                // �����ł���ꏊ���Ȃ�������I��
                if (transforms.Count == 0)
                    break;

                int rand = Random.Range(0, transforms.Count);

                Vector3 SpownPos = transforms[rand].position;

                enemyCount++;

                if (enemyCount < 11)
                {
                    // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
                    Instantiate(createPrefab, SpownPos, createPrefab.transform.rotation);
                    // �����ɓ����ꏊ���琶�����Ȃ�
                    transforms.RemoveAt(rand);
                }
            }

            time = 0f;
        }
        time = time + Time.deltaTime;
        //Debug.Log(time);
    }
}