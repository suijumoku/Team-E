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
    
    [SerializeField] float detectDistance;
    [SerializeField] float boundsPower = default!;

    // [SerializeField] ResultManager resultManager = default!;
    [SerializeField] PlayerController _playerController = default!;

    public Transform[] points;
    [SerializeField]
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
        
        player_ = GameObject.Find("Fine_Player");

        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        agent.autoBraking = false;

        InitArray(ref nextPoint, points.Length);

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        // 地点がなにも設定されていないときに返す
        if (points.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = points[nextPoint[destPoint]].position;

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどる
        if (++destPoint >= points.Length)
        {
            Shuffle(ref nextPoint);
            print("shuffe");


            destPoint = 0;
        }
    }
    void InitArray(ref int[] array, int length)
    {
        Array.Resize(ref array, length);

        for (int i = 0; i < length; i++)
        {
            array[i] = i;
        }
        Shuffle(ref array);
    }

    void Shuffle(ref int[] array)
    {
        for (var i = array.Length - 1; i > 0; --i)
        {
            // 0以上i以下のランダムな整数を取得
            // Random.Rangeの最大値は第２引数未満なので、+1することに注意
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
                    //GetComponent<Renderer>().material = chaseMaterial;
                }
                IsDetected = true;
            }
            else
            {
                if (IsDetected)
                {
                    //GetComponent<Renderer>().material = patrolMaterial;
                }
                IsDetected = false;
            }

            if (!IsDetected)
            {
                // エージェントが現目標地点に近づいてきたら、
                // 次の目標地点を選択
                if (!agent.pathPending && agent.remainingDistance < 1f)
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
        obj = GameObject.Find("Fine_Player");
        _playerController = obj.GetComponent<PlayerController>();   //フラグの情報を更新

        //小槌に当たった時
        if (collision.gameObject.tag == "Hammer" && _playerController.isAttack == true && _playerController.isHit == false)
        {
            Debug.Log("Hit");
            if (gameObject.tag == "Enemy")
            {
                //親子関係解除
                child.gameObject.GetComponent<BoxCollider>().enabled = true;
                child.gameObject.AddComponent<Rigidbody>();
                //child.gameObject.GetComponent<NavMeshAgent>().enabled = true;
                child.gameObject.GetComponent<EnemyScript>().enabled = true;
                //child.gameObject.tag = ("Enemy");
                transform.DetachChildren();

                NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
                navMeshAgent.enabled = false;

                _playerController.isHit = true;

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionY;

                gameObject.tag = "DarumaBall";

                Force(collision);

                //   resultManager.NormalHit();
            }

        }

        //飛んできた達磨に当たった時
        if (collision.gameObject.tag == "DarumaBall")
        {
            if (gameObject.tag == "Enemy")
            {
                //親子関係解除
                child.gameObject.GetComponent<BoxCollider>().enabled = true;
                child.gameObject.AddComponent<Rigidbody>();
                //child.gameObject.GetComponent<NavMeshAgent>().enabled = true;
                child.gameObject.GetComponent<EnemyScript>().enabled = true;
                //child.gameObject.tag = ("Enemy");
                transform.DetachChildren();
                Destroy(gameObject);

                //resultManager.DoubleHit();
            }
        }
        //飛んできた達磨の消去
        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "DarumaBall")
            {
                count++;
                if (count == 4)
                {
                    Destroy(gameObject);
                }
            }
        }

        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "DarumaBall" || collision.gameObject.tag == "Tourou")
        {
            if(gameObject.tag == "DarumaBall")
            {
                count++;
                if(count == 4)
                {
                    Destroy(gameObject);
                }
                Force2(collision);
            }
        }

    }

    void Force(Collision collision)
    {
        Debug.Log("a");
        // 衝突位置を取得する
        Vector3 hitPos = collision.contacts[0].point;

        // 衝突位置から自機へ向かうベクトルを求める
        Vector3 boundVec = this.transform.position - hitPos;

        // 逆方向にはねる
        Vector3 forceDir = boundsPower * boundVec.normalized;
        this.GetComponent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);

        Vector3 vector3 = player_.transform.forward;

        gameObject.GetComponent<Rigidbody>().AddForce(vector3 * boundsPower, ForceMode.Impulse);
    }

    void Force2(Collision collision)
    {
        // 衝突位置を取得する
        Vector3 hitPos = collision.contacts[0].point;

        // 衝突位置から自機へ向かうベクトルを求める
        Vector3 boundVec = this.transform.position - hitPos;

        // 逆方向にはねる
        Vector3 forceDir = boundsPower * boundVec.normalized;
        Debug.Log(forceDir);
        this.GetComponent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);
    }

}