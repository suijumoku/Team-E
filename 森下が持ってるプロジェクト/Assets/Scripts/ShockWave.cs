using System.Collections;
using System.Collections.Generic;
using UnityEditor.ProBuilder;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField] private ProBuilderEditor editor;
    float t=0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        transform.localScale=new Vector3(t,t,t);
    }
}
