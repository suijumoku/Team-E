using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform child;
    [SerializeField] Material patrolMaterial;
    [SerializeField] Material chaseMaterial;
    [SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    bool IsDetected = false;
    Rigidbody rigidBody;




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        agent.autoBraking = false;


        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        // 地点がなにも設定されていないときに返す
        if (points.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = points[destPoint].position;

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどる
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        float distance;

        distance = Vector3.Distance(transform.position, player.position);

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
            // エージェントが現目標地点に近づいてきたら、
            // 次の目標地点を選択
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
        else
        {
            agent.destination = player.position;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            child.gameObject.AddComponent<NavMeshAgent>();
            child.gameObject.AddComponent<Rigidbody>();

            //親子関係解除
            transform.DetachChildren();

            gameObject.tag = "Ball";

            MeshCollider collider = GetComponent<MeshCollider>();
            collider.isTrigger = true;
            gameObject.AddComponent<Rigidbody>();
            //オブジェクトの消去
            //Destroy(gameObject);
        }
    }
}