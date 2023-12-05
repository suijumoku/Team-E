using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform child;
    [SerializeField] Material patrolMaterial;
    [SerializeField] Material chaseMaterial;
    [SerializeField] float detectDistance;
    [SerializeField] float boundsPower = default!;

    // [SerializeField] ResultManager resultManager = default!;
    [SerializeField] PlayerController _playerController = default!;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    bool IsDetected = false;

    GameObject player_ = default!;
    GameObject obj = default;

    int count = 0;
    [SerializeField]
    int[] nextPoint = new int[0];


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //gameObject.AddComponent<Rigidbody>();
        player_ = GameObject.Find("Player");

        // autoBraking �𖳌��ɂ���ƁA�ڕW�n�_�̊Ԃ��p���I�Ɉړ����܂�
        //(�܂�A�G�[�W�F���g�͖ڕW�n�_�ɋ߂Â��Ă�
        // ���x�����Ƃ��܂���)
        agent.autoBraking = false;

        InitArray(ref nextPoint, points.Length);

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ�
        if (points.Length == 0)
            return;

        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�
        agent.destination = points[nextPoint[destPoint]].position;


        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�
        if (++destPoint>=points.Length)
        {
            Shuffle(ref nextPoint);

            destPoint = 0;
        }
    }
    void InitArray(ref int[] array,int length)
    {
        Array.Resize(ref array, length);

        for(int i=0;i<length;i++)
        {
            array[i] = i;
        }
        Shuffle(ref array);
    }

    void Shuffle(ref int[] array)
    {
        for (var i = array.Length - 1; i > 0; --i)
        {
            // 0�ȏ�i�ȉ��̃����_���Ȑ������擾
            // Random.Range�̍ő�l�͑�Q���������Ȃ̂ŁA+1���邱�Ƃɒ���
            var j = UnityEngine.Random.Range(0, i + 1);

            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }

    void Update()
    {
        float distance;

        distance = Vector3.Distance(transform.position, player_.transform.position);
        if (gameObject.tag == "Enemy")
        {
            if (distance < detectDistance)
            {
                if (!IsDetected)
                {
                    GetComponent<Renderer>().material = chaseMaterial;
                }
                IsDetected = true;
            }
            else
            {
                if (IsDetected)
                {
                    GetComponent<Renderer>().material = patrolMaterial;
                }
                IsDetected = false;
            }

            if (!IsDetected)
            {
                // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A
                // ���̖ڕW�n�_��I��
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GotoNextPoint();
                }
            }
            else
            {
                agent.destination = player_.transform.position;
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        obj = GameObject.Find("Player");
        _playerController = obj.GetComponent<PlayerController>();   //�t���O�̏����X�V
        
        //���Ƃɓ���������
        if (collision.gameObject.tag == "Hammer" && _playerController.isAttack == true && _playerController.isHit == false)
        {
            Debug.Log("Hit");
            if (gameObject.tag == "Enemy")
            {
                //�e�q�֌W����
                transform.DetachChildren();

                NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
                navMeshAgent.enabled = false;
                gameObject.AddComponent<Rigidbody>();

                _playerController.isHit = true;


                gameObject.tag = "DarumaBall";

                Force(collision);

                //   resultManager.NormalHit();
            }

        }

        //���ł����B���ɓ���������
        if (collision.gameObject.tag == "DarumaBall")
        {
            if (gameObject.tag == "Enemy")
            {
                //�e�q�֌W����
                transform.DetachChildren();
                NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
                navMeshAgent.enabled = false;
                gameObject.AddComponent<Rigidbody>();

               Destroy(gameObject);

                //   resultManager.DoubleHit();
            }
        }
        //���ł����B���̏���
        if (collision.gameObject.tag == "Enemy")
        {
            count++;
            if (gameObject.tag == "DarumaBall" && count == 4)
            {
                Destroy(gameObject);
            }
        }

    }

    void Force(Collision collision)
    {

        //// �Փˈʒu���擾����
        //Vector3 hitPos = collision.contacts[0].point;

        //// �Փˈʒu���玩�@�֌������x�N�g�������߂�
        //Vector3 boundVec = this.transform.position - hitPos;

        //// �t�����ɂ͂˂�
        //Vector3 forceDir = boundsPower * boundVec.normalized;
        //this.GetComponent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);

        Vector3 vector3 = player_.transform.forward;

        gameObject.GetComponent<Rigidbody>().AddForce(vector3 * boundsPower,ForceMode.Impulse);
    }

}