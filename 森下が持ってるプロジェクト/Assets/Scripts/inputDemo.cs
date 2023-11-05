using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class inputDemo : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
     
        //R Stick
        float rsh = Input.GetAxis("R_Stick_H");
        float rsv = Input.GetAxis("R_Stick_V");
        if ((rsh != 0) || (rsv != 0))
        {
            Debug.Log("R stick:" + rsh + "," + rsv);
        }
        //D-Pad
        float dph = Input.GetAxis("D_Pad_H");
        float dpv = Input.GetAxis("D_Pad_V");
        if ((dph != 0) || (dpv != 0))
        {
            Debug.Log("D Pad:" + dph + "," + dpv);
        }
        //Trigger
        float tri = Input.GetAxis("L_R_Trigger");
        if (tri > 0)
        {
            Debug.Log("R trigger:" + tri);
        }
        else if (tri < 0)
        {
            Debug.Log("L trigger:" + tri);
        }
        else
        {
            Debug.Log("  trigger:none");
        }
    }
}
