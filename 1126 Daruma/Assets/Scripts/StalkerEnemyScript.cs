using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StalkerEnemyScript : MonoBehaviour
{
    //GameObject型を変数targetで宣言します。
    public GameObject target;
    GameObject player_ = default!;

    [SerializeField] Transform child;
    
   
    [SerializeField] float boundsPower = default!;

    [SerializeField] ResultManager resultManager = default!;
    [SerializeField] PlayerController _playerController = default!;

    private NavMeshAgent nav; 
   
    GameObject obj = default;

    int count = 0;

    // Start is called before the first frame update

    void Awake()
    {
        player_ = GameObject.Find("Fine_Player");
    }
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Enemy")
        {

            nav.SetDestination(player_.transform.position);
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
                child.gameObject.GetComponent<StalkerEnemyScript>().enabled = true;
                //child.gameObject.tag = ("Enemy");
                transform.DetachChildren();
                _playerController.isHit = true;

                NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
                navMeshAgent.enabled = false;

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionY;

                gameObject.tag = "DarumaBall";

                Force(collision);

                resultManager.NormalHit();
            }
            if(gameObject.tag == "DarumaBall")
            {
                Force(collision);
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
                child.gameObject.GetComponent<StalkerEnemyScript>().enabled = true;
                //child.gameObject.tag = ("Enemy");
                transform.DetachChildren();
                gameObject.tag = "DarumaBall";
                NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
                navMeshAgent.enabled = false;
                Force2(collision);
                resultManager.DoubleHit();

                //Destroy(gameObject);


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

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "DarumaBall" || collision.gameObject.tag == "Tourou")
        {
            if (gameObject.tag == "DarumaBall")
            {
                count++;
                if (count == 4)
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
