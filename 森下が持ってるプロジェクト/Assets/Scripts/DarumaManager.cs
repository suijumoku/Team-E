using UnityEngine;
using UnityEngine.AI;

public class DarumaManager : MonoBehaviour
{
    [SerializeField] GameObject daruma = default!;
    
    private NavMeshAgent navMeshAgent;
    private EnemyScript enemyScript;

    void Awake()
    {
        navMeshAgent = daruma.GetComponent<NavMeshAgent>();
        //daruma.AddComponent<Rigidbody>();
        navMeshAgent.enabled = false;
        enemyScript = daruma.GetComponent<EnemyScript>();
        enemyScript.enabled = false;
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
                enemyScript.enabled = true;
                gameObject.tag = "Enemy";
            }
        }
    }
}