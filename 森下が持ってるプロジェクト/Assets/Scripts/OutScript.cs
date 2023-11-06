//古澤
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutScript : MonoBehaviour
{
    [SerializeField] GameObject respawnP = default!;
    [SerializeField] GameObject player = default!;
    [SerializeField] Miss miss = default!;
    [SerializeField] PlayerController playerController;

 
    //[SerializeField] IntentionScript intentionScript = default!;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = playerController.GetComponent<PlayerController>();

        //_ChangeImage =  GetComponent<ChangeImage>();
    }
    private void Update()
    {
       // Debug.Log(player.transform.position);
    }

    void OnTriggerEnter(Collider other)     //落下した時の処理 エリアに入ったらサウンド->出たら復活
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.fall();        //outAreaにGameManager入れて音鳴らそうとするとなぜかバグるから遠回しに再生
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            miss.InOrder();
            GameObject c = other.GetComponent<GameObject>();
            other.transform.localPosition = respawnP.transform.localPosition;         
        }
    }
}
