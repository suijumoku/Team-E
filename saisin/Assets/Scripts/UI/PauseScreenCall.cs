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

        // 入力不可なら終わり
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
            Time.timeScale = 1; //もう一度menuボタン押すと動き出す
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
