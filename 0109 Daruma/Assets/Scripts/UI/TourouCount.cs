using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourouCount : MonoBehaviour
{
    //const int tensPlace = 0, onePlace = 1;

    [Header("�X�R�A")]
    [SerializeField] Image[] scoreImg;  //[i][j] i:�e���q j:�\�̈ʂ���̈�

    //[Header("�X�R�A�̐���")]
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
        resultManager = obj.GetComponent<ResultManager>();  //resultmanager�̍X�V
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
