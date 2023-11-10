using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform child;
    [SerializeField] Material patrolMaterial;
    [SerializeField] Material chaseMaterial;
    [SerializeField] float detectDistance;
    [SerializeField] ResultManager resultManager = default!;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    bool IsDetected = false;
    GameObject player_ = default!;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player_ = GameObject.Find("Team_E_Player");
       
        // autoBraking �𖳌��ɂ���ƁA�ڕW�n�_�̊Ԃ��p���I�Ɉړ����܂�
        //(�܂�A�G�[�W�F���g�͖ڕW�n�_�ɋ߂Â��Ă�
        // ���x�����Ƃ��܂���)
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ�
        if (points.Length == 0)
            return;

        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�
        agent.destination = points[destPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        float distance;

        distance = Vector3.Distance(transform.position, player_.transform.position);

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
            if (!agent.pathPending && agent.remainingDistance < 2f)
            {
                GotoNextPoint();
            }
        }
        else
        {
            agent.destination = player_.transform.position;
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        //���Ƃɓ���������
        if (collision.gameObject.tag == "Ball")
        {
            child.gameObject.AddComponent<NavMeshAgent>();
            child.gameObject.AddComponent<Rigidbody>();

            //�e�q�֌W����
            transform.DetachChildren();

            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;

            gameObject.tag = "DarumaBall";

            gameObject.AddComponent<Rigidbody>();

            resultManager.NormalHit();




        }
        //���ł����B���ɓ���������
        if (collision.gameObject.tag == "DarumaBall")
        {
            if (gameObject.tag == "Enemy")
            {
                child.gameObject.AddComponent<NavMeshAgent>();
                child.gameObject.AddComponent<Rigidbody>();

                //�e�q�֌W����
                transform.DetachChildren();
                Destroy(gameObject);

                resultManager.DoubleHit();
            }
        }
        //���ł����B���̏���
        if(collision.gameObject.tag == "Enemy")
        {
            if(gameObject.tag == "DarumaBall")
            {
                Destroy(gameObject);
            }
        }

    }
}