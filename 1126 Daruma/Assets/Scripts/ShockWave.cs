using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ProBuilder;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [Header("ŽË’ö”¼Œa")] public float LengthRadius;
    [Header("ŽžŠÔ")] public float MoveTime = 2f;
    float time;
    float Move;

    private void Start()
    {
        Move = LengthRadius / MoveTime;
    }
    private void Update()
    {
        Vector3 scl = transform.localScale;
        time += Time.deltaTime;
        if (time < MoveTime)
        {
            scl.x += Move * Time.deltaTime;
            scl.z += Move * Time.deltaTime;
            transform.localScale = scl;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
