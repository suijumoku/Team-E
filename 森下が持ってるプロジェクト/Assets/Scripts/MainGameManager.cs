using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    //[SerializeField] PlayerController _PlayerController;
    //[SerializeField] ResultManager _resultManager;
    [SerializeField] FadeAndSceneMove _fadeAndSceneMove;

    //private MainGameManager instance;

    //public void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}

    void Start()
    {
        //SceneManager.activeSceneChanged += ActiveSceneChanged;
        //_PlayerController = GetComponent<PlayerController>();
       // _resultManager = _resultManager.GetComponent<ResultManager>();
        _fadeAndSceneMove = _fadeAndSceneMove.GetComponent<FadeAndSceneMove>();
    }    

    public void Defeat()
    {
        _fadeAndSceneMove.FadeStart();            
    }

    public void Clear(int life)
    {
      
    }

    //void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    //{
    //    Debug.Log(thisScene.name);
    //    Debug.Log(nextScene.name);
    //    if (nextScene.name == "Result")
    //    {
    //        _resultManager.IndicateScore();
    //    }
    //}
}
