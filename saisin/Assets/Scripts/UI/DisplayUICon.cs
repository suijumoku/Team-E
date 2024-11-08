using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUICon: MonoBehaviour
{
    [Header("ï\é¶Ç∑ÇÈUI")] public GameObject displayUI;
    [SerializeField]
    private bool Ondisplay;
    // Start is called before the first frame update
    void Start()
    {
        displayUI.SetActive(false);
        Ondisplay = false;
    }

    public void DisplayControl ()
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

    public bool isOnDisplay()
        { return Ondisplay; }
}
