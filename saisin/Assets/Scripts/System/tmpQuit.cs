using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpQuit : MonoBehaviour
{
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            quitGame();
        }
    }
    public void quitGame()
    {
        print("Quit��������܂���");

   �@ #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
   �@ #else
            Application.Quit();
   �@ #endif
    }
 
}
