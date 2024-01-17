using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourouCount : MonoBehaviour
{ 
    [Header("�X�R�A")]
    [SerializeField] Image[] scoreImg;  //[i][j] i:�e���q j:�\�̈ʂ���̈� 

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
     �@//�����Ƃ��ꂢ�ɏ�������

       //breakTourouValue��controlValue�������邱�Ƃœ��U�����邽�тɈ�x�������������ʂ�
        if (scoreArray[0] > scoreArray[0] - ResultManager.breakTourouValue + controlValue)
        {           

            //breakTourouValue�͓��U�j�󂲂ƂɃJ�E���g�������Ă������炻��ɔ����l�𑝂₷
            scoreArray[0] -= ResultManager.breakTourouValue - controlValue;
           
            //resultManager�ɃA�^�b�`���Ă��鐔���̉摜������
            scoreImg[0].sprite = resultManager.Numbers[scoreArray[0]];           
           
            controlValue++;
        }   
    }


}
