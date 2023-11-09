//���V
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
            if (currentCount >= missCount) return;

            _MainGameManager.InOrder(currentCount);
            //GameObject c = other.GetComponent<GameObject>();
            other.transform.localPosition = respawnP.transform.localPosition;

            currentCount++;
        }
    }
}
