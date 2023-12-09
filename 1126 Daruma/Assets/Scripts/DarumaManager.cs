using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class DarumaManager : MonoBehaviour
{
    [SerializeField] GameObject daruma = default!;
    [SerializeField] GameObject parent = default!;

    private NavMeshAgent navMeshAgent;
    private EnemyScript enemyScript;
    private DarumaManager darumaManager;

    void Awake()
    {
        navMeshAgent = daruma.GetComponent<NavMeshAgent>();
        //navMeshAgent.enabled = false;
        enemyScript = daruma.GetComponent<EnemyScript>();
        enemyScript.enabled = false;
        darumaManager = daruma.GetComponent<DarumaManager>();
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
            if (gameObject.tag == "Untagged")
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                Destroy(rb);

                navMeshAgent.enabled = true;
                enemyScript.enabled = true;

                gameObject.tag = "Enemy";

                darumaManager.enabled = false;

            }
        }
    }
}