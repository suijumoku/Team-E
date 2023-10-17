using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUICon: MonoBehaviour
{
    [Header("ï\é¶Ç∑ÇÈUI")] public GameObject displayUI;
    public bool Ondisplay = false;
    // Start is called before the first frame update
    void Start()
    {
        displayUI.SetActive(false);
    }

    public void displayControl ()
    {
        if (!Ondisplay)
        {
            displayUI.SetActive(true);
            Ondisplay = true;
        }
        else
        {
            displayUI.SetActive(false);
            Ondisplay=false;
        }
    }
}
