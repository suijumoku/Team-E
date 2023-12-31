using System.Collections.Generic;
using UnityEngine;

public class SpownenemyScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��������GameObject")]
    private GameObject[] createPrefab;
    [SerializeField]
    [Tooltip("��������ꏊ")]
    private Transform[] SpownPoints;
    [SerializeField]
    [Tooltip("��������Ԋu")]
    private float SpownTime = 5;
    [SerializeField]
    [Tooltip("�G�̏o�����")]
    private int enemyNumlimit = 10;
    [SerializeField]
    [Tooltip("�G����x�ɏo�����鐔")]
    private int enemyOnceSpown = 2;
    [SerializeField]
    [Tooltip("�G�̃J�E���g��ʁX�ɂ���")]
    private bool anotherEnemyCount=false;
    [SerializeField]
    [Tooltip("�G���o�������Ƃ���SE")]
    private AudioClip spownSE;


    [SerializeField]
    [Tooltip("��������Enemy�^�O��GameObject")]
    private GameObject[] createdEnemyTagPrefab;
    private int enemyCount = 0;

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

            if(transforms.Count!=0)
            {
                for (int i = 0; i < enemyOnceSpown; i++)
                {
                    // �����ł���ꏊ���Ȃ�������I��
                    if (transforms.Count == 0)
                        break;

                    if(!anotherEnemyCount)
                    enemyCount = EnemyCountCheck("Enemy");

                    if (enemyCount < enemyNumlimit)
                    {
                        int rand = Random.Range(0, transforms.Count);
                        int darumaRand = Random.Range(0, createPrefab.Length);

                        Vector3 SpownPos = transforms[rand].position;

                        // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
                        Instantiate(createPrefab[darumaRand], SpownPos, createPrefab[darumaRand].transform.rotation);
                        GameManager.instance.PlaySE(spownSE);
                        // �����ɓ����ꏊ���琶�����Ȃ�
                        transforms.RemoveAt(rand);

                        enemyCount++;

                    }
                }
                print("����");
                print(transforms.Count);
            }
            else
            {
                print("�����s��");
            }

            time = 0f;
        }
        time = time + Time.deltaTime;
        if(!anotherEnemyCount)
        enemyCount = EnemyCountCheck("Enemy");

        //Debug.Log(time);
    }

    int EnemyCountCheck(string tagname)
    {
        createdEnemyTagPrefab = GameObject.FindGameObjectsWithTag(tagname);
        return createdEnemyTagPrefab.Length;
    }
}