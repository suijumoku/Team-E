using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstSelectButton : MonoBehaviour
{
    public bool OnLoadScene;
    [SerializeField] private Button firstbutton;
    void OnEnable()
    {
        Debug.Log("OnEnable");
        onSelect();
    }

    private void Start()
    {
        if (OnLoadScene)
            onSelect();
    }

    public void onSelect()
    {
        firstbutton.Select();
    }
}
