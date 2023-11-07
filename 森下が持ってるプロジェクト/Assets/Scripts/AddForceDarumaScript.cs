<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceDarumaScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            float boundsPower = 20.0f;

            // 衝突位置を取得する
            Vector3 hitPos = collision.contacts[0].point;

            // 衝突位置から自機へ向かうベクトルを求める
            Vector3 boundVec = this.transform.position - hitPos;

            // 逆方向にはねる
            Vector3 forceDir = boundsPower * boundVec.normalized;
            this.GetComponent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);

        }
    }
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceDarumaScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            float boundsPower = 10.0f;

            // 衝突位置を取得する
            Vector3 hitPos = collision.contacts[0].point;

            // 衝突位置から自機へ向かうベクトルを求める
            Vector3 boundVec = this.transform.position - hitPos;

            // 逆方向にはねる
            Vector3 forceDir = boundsPower * boundVec.normalized;
            this.GetComponent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);

        }
    }
>>>>>>> 93259010dd9cca53a340056aae408d0fbac9161c
}