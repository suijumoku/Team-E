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
            // エージェントが現目標地点に近づいてきたら、
            // 次の目標地点を選択
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
        //小槌に当たった時
        if (collision.gameObject.tag == "Ball")
        {
            child.gameObject.AddComponent<NavMeshAgent>();
            child.gameObject.AddComponent<Rigidbody>();

            //親子関係解除
            transform.DetachChildren();

            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;

            gameObject.tag = "DarumaBall";

            gameObject.AddComponent<Rigidbody>();

            resultManager.NormalHit();




        }
        //飛んできた達磨に当たった時
        if (collision.gameObject.tag == "DarumaBall")
        {
            if (gameObject.tag == "Enemy")
            {
                child.gameObject.AddComponent<NavMeshAgent>();
                child.gameObject.AddComponent<Rigidbody>();

                //親子関係解除
                transform.DetachChildren();
                Destroy(gameObject);

                resultManager.DoubleHit();
            }
        }
        //飛んできた達磨の消去
        if(collision.gameObject.tag == "Enemy")
        {
            if(gameObject.tag == "DarumaBall")
            {
                Destroy(gameObject);
            }
        }

    }
}