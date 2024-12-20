using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstSelectButton : MonoBehaviour
{
    
    [SerializeField] private Button firstbutton;
    [Header("有効になったら実行する")][SerializeField] bool OnEneble;
    [Header("シーンが読み込まれたら実行する")][SerializeField] bool OnLoadScene;
    void OnEnable()
    {
        if (OnEneble)
        {
            Debug.Log("OnEnableFirstButtonSelect");
            onSelect();
        }
    }

    private void Start()
    {
        if (OnLoadScene)
        {
            Debug.Log("LoadSceneFirstButtonSelect");
            onSelect();
        }
    }

    public void onSelect()
    {
        firstbutton.Select();
    }
}
