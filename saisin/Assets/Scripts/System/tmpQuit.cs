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
        print("Quitが押されました");

   　 #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
   　 #else
            Application.Quit();
   　 #endif
    }
 
}
