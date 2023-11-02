//å√‡V
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutScript : MonoBehaviour
{
    [SerializeField] GameObject respawnP = default!;
    [SerializeField] GameObject player = default!;
    [SerializeField] Miss miss = default!;
    //[SerializeField] IntentionScript intentionScript = default!;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        //_ChangeImage =  GetComponent<ChangeImage>();
    }
    private void Update()
    {
       // Debug.Log(player.transform.position);
    }

    void OnTriggerEnter(Collider other)     //óéâ∫ÇµÇΩéûÇÃèàóù
    {
        if (other.gameObject.CompareTag("Player"))
        {
            miss.InOrder();
            GameObject c = other.GetComponent<GameObject>();          
            other.transform.localPosition = respawnP.transform.localPosition;          
        }
    }
}
