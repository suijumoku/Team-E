using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == playerTag)
        {
            isOn = true;
            print("isOn");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == playerTag)
        {
            isOn = false;
        }
    }

}
