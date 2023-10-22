//å√‡V
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutScript : MonoBehaviour
{
    [SerializeField] GameObject respawnP;
    [SerializeField] GameObject player;
    [SerializeField] private ChangeImage _ChangeImage1, _ChangeImage2, _ChangeImage3;
    private int failC = 0;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        //_ChangeImage =  GetComponent<ChangeImage>();
    }
    private void Update()
    {
       // Debug.Log(player.transform.position);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InOrder();
            GameObject c = other.GetComponent<GameObject>();          
            other.transform.localPosition = respawnP.transform.localPosition;
          
        }
    }
    private void InOrder()
    {
        if (failC == 0)
        {
            _ChangeImage1.StartCoroutine("Miss");
            failC++;
        }
        else if (failC == 1)
        {
            _ChangeImage2.StartCoroutine("Miss");
            failC++;
        }
        else if (failC == 2)
        {
            _ChangeImage3.StartCoroutine("Miss");
            failC++;
        }
    }
}
