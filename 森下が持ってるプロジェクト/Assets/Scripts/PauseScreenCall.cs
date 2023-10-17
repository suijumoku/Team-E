using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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

        if (current.escapeKey.wasPressedThisFrame)
        {
            uICon.displayControl();
        }
        if (uICon.Ondisplay)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        uICon.displayControl();
    }

    public void RetuenMenu()
    {
        Time.timeScale = 1;
        uICon.displayControl();
        SceneManager.LoadScene("Menu");
    }
}
