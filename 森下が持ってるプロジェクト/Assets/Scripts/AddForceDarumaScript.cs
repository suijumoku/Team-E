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

            // �Փˈʒu���擾����
            Vector3 hitPos = collision.contacts[0].point;

            // �Փˈʒu���玩�@�֌������x�N�g�������߂�
            Vector3 boundVec = this.transform.position - hitPos;

            // �t�����ɂ͂˂�
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

            // �Փˈʒu���擾����
            Vector3 hitPos = collision.contacts[0].point;

            // �Փˈʒu���玩�@�֌������x�N�g�������߂�
            Vector3 boundVec = this.transform.position - hitPos;

            // �t�����ɂ͂˂�
            Vector3 forceDir = boundsPower * boundVec.normalized;
            this.GetComponent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);

        }
    }
>>>>>>> 93259010dd9cca53a340056aae408d0fbac9161c
}