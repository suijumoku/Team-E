using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarumaManager : MonoBehaviour
{
    [SerializeField] GameObject daruma = default!;

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            navMeshAgent.enabled = true;
        }
    }
}