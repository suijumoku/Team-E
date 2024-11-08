using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activetuui : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject Object;
    [SerializeField] bool isActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isActive = Object.activeSelf;
        print(Object.activeSelf);
    }
}
