using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDaruma : MonoBehaviour
{
    [Header("ÉvÉåÉCÉÑÅ[")][SerializeField] Transform P;


    void Start()
    {
        if (Input.GetMouseButtonDown(0) && P != null)
        {
            Jump(P.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Jump(Vector3 PlayerPosition)
    {
        Vector3 pos = P.transform.position;

    }
}
