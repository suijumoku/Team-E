////古澤
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    //同じサイズの画像を重ねて表示のON,OFFで切り替える
    [SerializeField] Image trueImage, falseImage;
    [SerializeField] int ThisLife;
    [SerializeField] float duration = 0.2f;  //点滅の感覚(要調整)

    private ChangeImage instance;
            
    private float time = 0.0f;             // 経過時間を格納する変数    
    private bool changeF1 = false, changeF2 = false;
   
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        trueImage.GetComponent<Image>().enabled = true;
        falseImage.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
     
    }
    public void ChangeImg(Image off, Image on)
    {
        off.enabled = false;
        on.enabled = true;
        Debug.Log("Changed");
    }

    public IEnumerator Miss()
    {
        Debug.Log("Miss");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("CountStart");
            while (true)
            {
            
                if (Mathf.Approximately(time, 0.0f))
                {
                    ChangeImg(trueImage, falseImage);
                }
                time += Time.deltaTime;
                if (time >= duration && !changeF1)
                {
                    ChangeImg(falseImage, trueImage);
                    changeF1 = true;
                }
                else if (time >= duration * 2 && !changeF2)
                {
                    ChangeImg(trueImage, falseImage);
                    changeF2 = true;
                }
           
                yield return null;
            }       

              
    }
}
