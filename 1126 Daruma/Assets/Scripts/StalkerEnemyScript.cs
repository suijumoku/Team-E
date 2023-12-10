using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerEnemyScript : MonoBehaviour
{
    //GameObject型を変数targetで宣言します。
    public GameObject target;
    GameObject player_ = default!;

    // Start is called before the first frame update

    void Awake()
    {
        player_ = GameObject.Find("New_EPlayer");
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        Quaternion lookRotation = Quaternion.LookRotation(player_.transform.position - transform.position, Vector3.up);

        lookRotation.z = 0;
        lookRotation.x = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

        Vector3 p = new Vector3(0f, 0f, 0.023f);

        transform.Translate(p);
    }


}
