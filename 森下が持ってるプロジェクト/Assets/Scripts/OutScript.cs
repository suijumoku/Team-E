//���V
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

    void OnTriggerEnter(Collider other)     //�����������̏��� �G���A�ɓ�������T�E���h->�o���畜��
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.fall();        //outArea��GameManager����ĉ��炻���Ƃ���ƂȂ����o�O�邩�牓�񂵂ɍĐ�
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             _MainGameManager.Miss();  //���C�t0
                       
            player.transform.localPosition = respawnP.transform.localPosition;              
            
        }
    }

}
