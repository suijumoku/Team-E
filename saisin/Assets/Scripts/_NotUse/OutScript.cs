//���V
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class OutScript : MonoBehaviour
{
    [SerializeField] Transform respawnP;
    //[SerializeField] BlinkingScript blinkingScript = default!;
    [FormerlySerializedAs("_MainGameManager")] [SerializeField] IngameManager ingameManager = default!;
    [SerializeField] Transform player = default!;
    [SerializeField] PlayerController playerController;
    bool isAlive;

    void OnTriggerEnter(Collider other)     //�����������̏��� �G���A�ɓ�������T�E���h->�o���畜��
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.fall();        //outArea��GameManager����ĉ��炻���Ƃ���ƂȂ����o�O�邩�牓�񂵂ɍĐ�
        }
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("DarumaBall"))
        {
          //  Debug.Log("Destroyed" + other.gameObject.tag);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             ingameManager.Miss();  //���C�t0
                       
            player.transform.position = respawnP.transform.position;              
            
        }    
    }

}
