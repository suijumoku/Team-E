using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class DarumaManager : MonoBehaviour
{
    [SerializeField] GameObject daruma = default!;
   // [SerializeField] GameObject parent = default!;

    private NavMeshAgent navMeshAgent;
   // private BoxCollider collider;
    private EnemyScript enemyScript;
    private DarumaManager darumaManager;

    void Awake()
    {
        //navMeshAgent = daruma.GetComponent<NavMeshAgent>();
        //navMeshAgent.enabled = false;
        //collider = daruma.GetComponent<BoxCollider>();
        //enemyScript = daruma.GetComponent<EnemyScript>();
        //enemyScript.enabled = false;
        //darumaManager = daruma.GetComponent<DarumaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    navMeshAgent.enabled = true;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("n");
            if (gameObject.tag == "Untagged")
            {
               // Debug.Log("b");
                daruma.tag = "Enemy";
                NavMeshAgent navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
                navMeshAgent.enabled = true;
                //enemyScript.enabled = true;
                //collider.enabled = true;

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeRotation;

                

                //darumaManager.enabled = false;

            }
        }
    }
}