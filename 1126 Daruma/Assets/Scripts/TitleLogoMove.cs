using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogoMove : MonoBehaviour
{
    public float MoveLength;
    public float MoveTime = 2f;
    float time;
    float x;
    float Afterx;
    float Move;

    private void Start()
    {
        x = transform.position.x;
        Afterx = transform.position.x + MoveLength;
        Move = MoveLength / MoveTime;
    }
    private void Update()
    {
        Vector3 pos = transform.position;
        time += Time.deltaTime;
        if (time < MoveTime)
        {
            pos.x += Move * Time.deltaTime;
            transform.position = pos;
        }
    }
}
