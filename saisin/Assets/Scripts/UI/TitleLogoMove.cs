using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogoMove : MonoBehaviour
{
    public float moveLength;
    public float moveTime = 2f;

    [SerializeField]
    private float time;
    [SerializeField]
    private float originalx;
    [SerializeField]
    private float moveAfterx;

    private void Start()
    {
        time = 0;    
        originalx = transform.localPosition.x;
        moveAfterx=transform.localPosition.x+moveLength;
    }
    private void Update()
    {
        Vector3 pos = transform.localPosition;
        if (time/moveTime<1)
        {
            time += Time.deltaTime;

            pos.x = Mathf.Lerp(originalx,moveAfterx,time/moveTime);
            transform.localPosition = pos;
        }
    }
}
