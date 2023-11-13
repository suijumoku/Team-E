//古澤
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutScript : MonoBehaviour
{
    [SerializeField] Transform respawnP;
    //[SerializeField] BlinkingScript blinkingScript = default!;
    [SerializeField] MainGameManager _MainGameManager = default!;
    [SerializeField] Transform player = default!;
    [SerializeField] PlayerController playerController;
    bool isAlive;

    private void Start()
    {
    
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
             _MainGameManager.Miss();  //ライフ0
                       
            player.transform.localPosition = respawnP.transform.localPosition;              
            
        }
    }

}
