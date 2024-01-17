using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourouCount : MonoBehaviour
{ 
    [Header("スコア")]
    [SerializeField] Image[] scoreImg;  //[i][j] i:親か子 j:十の位か一の位 

    [SerializeField] int tourouMax = default!;
    [SerializeField] ResultManager resultManager = default!;
    GameObject obj = default;
    int[] scoreArray;
    int controlValue = 0;
 
    void Start()
    {
        scoreArray = new int[1] { tourouMax };
        controlValue = 0;
        obj = GameObject.Find("ResultManager");
        resultManager = obj.GetComponent<ResultManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
     　//もっときれいに書けそう

       //breakTourouValueにcontrolValueを加えることで灯篭が壊れるたびに一度だけ条件文が通る
        if (scoreArray[0] > scoreArray[0] - ResultManager.breakTourouValue + controlValue)
        {           

            //breakTourouValueは灯篭破壊ごとにカウントが増えていくからそれに伴い値を増やす
            scoreArray[0] -= ResultManager.breakTourouValue - controlValue;
           
            //resultManagerにアタッチしてある数字の画像を入れる
            scoreImg[0].sprite = resultManager.Numbers[scoreArray[0]];           
           
            controlValue++;
        }   
    }


}
