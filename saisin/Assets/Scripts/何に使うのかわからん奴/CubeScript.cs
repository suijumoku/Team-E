using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    [SerializeField] GameObject firingPoint;

    [SerializeField] GameObject bullet;

    [SerializeField] float speed1 = 30f;
    float speed = 3.0f;
    EnemyScript enemyScript;

    void Update()
    {
        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= speed * transform.forward * Time.deltaTime;
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= speed * transform.right * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LauncherShot();
        }


    }

    private void LauncherShot()
    {

        Vector3 bulletPosition = firingPoint.transform.position;

        GameObject newBall = Instantiate(bullet, bulletPosition, transform.rotation);

        Vector3 direction = newBall.transform.forward;

        newBall.GetComponent<Rigidbody>().AddForce(direction * speed1, ForceMode.Impulse);

        Destroy(newBall, 0.8f);

    }
}