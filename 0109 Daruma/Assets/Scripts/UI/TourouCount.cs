using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourouCount : MonoBehaviour
{
    //const int tensPlace = 0, onePlace = 1;

    [Header("スコア")]
    [SerializeField] Image[] scoreImg;  //[i][j] i:親か子 j:十の位か一の位

    //[Header("スコアの数字")]
    //[SerializeField] public Sprite[] Numbers;

    [SerializeField] int tourouMax = default!;
    [SerializeField] ResultManager resultManager = default!;
    GameObject obj = default;
    int[] scoreArray;
    int test = 0;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        scoreArray = new int[1] { tourouMax };
        test = 0;
        obj = GameObject.Find("ResultManager");
        resultManager = obj.GetComponent<ResultManager>();  //resultmanagerの更新
    }

    // Update is called once per frame
    void Update()
    {
     
        if (scoreArray[0] > scoreArray[0] - ResultManager.breakTourouValue + test)
        {
            //if (test == 2)
            //    test--;
            scoreArray[0] -= ResultManager.breakTourouValue - test;
           
            scoreImg[0].sprite = resultManager.Numbers[scoreArray[0]];

            scoreImg[0].enabled = true;
           
            test++;
        }   
    }


}
