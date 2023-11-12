using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{

    [SerializeField] FadeAndSceneMove _fadeAndSceneMove;

    [SerializeField] int missCount = default!;
    [SerializeField] private BlinkingScript blinkingScript = default!;
    [SerializeField] ResultManager resultManager = default!;

    public bool isClear = false, isDefeat = false, isInvincible = false; //無敵時間中かどうか

    private int currentCount = 0;

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

       // Time.timeScale = 1.0f;
        
        isClear = false; isDefeat = false;
    }

    private void Update()
    {       

        if (isDefeat)
        {
            
            Debug.Log("isDefeat = " + isDefeat);
            Defeat();
            isDefeat = false;
          
        }
    }
    public void Miss()
    {
        if (currentCount >= missCount) return;

        InOrder(currentCount);
        //GameObject c = other.GetComponent<GameObject>();     

        currentCount++;       
    }

    public void InOrder(int i)
    {
        blinkingScript.StartCoroutine(blinkingScript.DamageIndication(i));      
    }

    public void Defeat()
    {
        _fadeAndSceneMove.FadeStart();   
    }

    public void Clear(int life)
    {
        if (blinkingScript.life == 3)
        {
            resultManager.NoDmgBonus(); //ライフが３残ってたらノーダメボーナス
        }          

        _fadeAndSceneMove.FadeStart();

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
