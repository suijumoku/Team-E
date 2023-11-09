//古澤
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutScript : MonoBehaviour
{
    [SerializeField] GameObject respawnP;
    [SerializeField] GameObject player;
    //[SerializeField] BlinkingScript blinkingScript = default!;
    [SerializeField] MainGameManager _MainGameManager = default!;
    [SerializeField] int missCount = default!;
    [SerializeField] PlayerController playerController;

    private int currentCount = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
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
            if (currentCount >= missCount) return;

            _MainGameManager.InOrder(currentCount);
            //GameObject c = other.GetComponent<GameObject>();
            other.transform.localPosition = respawnP.transform.localPosition;

            currentCount++;
        }
    }
}
