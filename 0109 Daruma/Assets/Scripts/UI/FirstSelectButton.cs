using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstSelectButton : MonoBehaviour
{
    
    [SerializeField] private Button firstbutton;
    [Header("�L���ɂȂ�������s����")][SerializeField] bool OnEneble;
    [Header("�V�[�����ǂݍ��܂ꂽ����s����")][SerializeField] bool OnLoadScene;
    void OnEnable()
    {
        if (OnEneble)
        {
            Debug.Log("OnEnable");
            onSelect();
        }
    }

    private void Start()
    {
        if (OnLoadScene)
        {
            Debug.Log("OnLoadScene");
            onSelect();
        }
    }

    public void onSelect()
    {
        firstbutton.Select();
    }
}
