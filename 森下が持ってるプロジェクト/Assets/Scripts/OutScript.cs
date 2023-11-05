//���V
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
            miss.InOrder();
            GameObject c = other.GetComponent<GameObject>();
            other.transform.localPosition = respawnP.transform.localPosition;         
        }
    }
}
