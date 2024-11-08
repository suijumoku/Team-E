using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;

public class PauseScreenCall : MonoBehaviour
{
    [SerializeField] private DisplayUICon uICon;

    // Start is called before the first frame update
    void Start()
    {
        uICon=GetComponent<DisplayUICon>();
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        // ���͕s�Ȃ�I���
        if (GameManager.instance.ReturnInputState() != InputState.OnInput)
            return;
        if (current.escapeKey.wasPressedThisFrame||Input.GetButtonDown("Menu"))
        {
            uICon.DisplayControl();
        }
        if (uICon.isOnDisplay())
            Time.timeScale = 0;

        if (uICon.isOnDisplay() == false && Time.timeScale == 0)
        {
            Time.timeScale = 1; //������xmenu�{�^�������Ɠ����o��
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        uICon.DisplayControl();
    }

    public void RetuenMenu()
    {
        Time.timeScale = 1;
        uICon.DisplayControl();
        SceneManager.LoadScene("Menu");
    }
}
