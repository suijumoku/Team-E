using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarumaHeadScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] ResultManager resultManager;
    GameObject obj = default;

    private void Awake()
    {
        obj = GameObject.Find("ResultManager");
        resultManager = obj.GetComponent<ResultManager>();  //resultmanager�̃A�^�b�`
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("b");
            Destroy(gameObject);
            resultManager.BeatDaruma();
            Debug.Log("BeatDaruma!");
        }
    }
}