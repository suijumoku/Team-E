using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarumaManager : MonoBehaviour
{
    [SerializeField] GameObject daruma = default!;
    [SerializeField] ResultManager resultManager;
    private NavMeshAgent navMeshAgent;
    void Awake()
    {
        navMeshAgent = daruma.GetComponent<NavMeshAgent>();
        daruma.GetComponent<Rigidbody>();
        navMeshAgent.enabled = false;
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
        if(collision.gameObject.tag == "Ground")
        {
            if (gameObject.tag == "Untagged")
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                Destroy(rb);
                navMeshAgent.enabled = true;
                gameObject.tag = "Enemy";

                resultManager.BeatDaruma();
               
            }
        }
    }
}