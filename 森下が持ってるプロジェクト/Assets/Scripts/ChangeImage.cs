////古澤
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    //同じサイズの画像を重ねて表示のON,OFFで切り替える
    [SerializeField] Material default_material;
    [SerializeField] Material translucent_material;
    [SerializeField] private GameObject player = default!;

    [SerializeField] Image trueLife = default!;
    [SerializeField] Image falseLife = default!;
    [SerializeField] float duration = 0.2f;  //点滅の感覚(要調整)       
    [SerializeField] float[] timeDelay = default!;
    private ChangeImage instance;


    private float time = 0.0f;             // 経過時間を格納する変数    
    private bool changeF1 = false;
    private bool changeF2 = false;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        trueLife.GetComponent<Image>().enabled = true;
        falseLife.GetComponent<Image>().enabled = false;    
    }

    public void ChangeImg(Image falseImage, Image trueImage)
    {
        falseImage.enabled = false;
        trueImage.enabled = true;
        //Debug.Log("Changed");
    }
    private IEnumerator Miss()
    {
        player = GameObject.FindWithTag("Player");
        player.gameObject.GetComponent<Renderer>().material = default_material;
        time = 0.0f;
        changeF1 = false;
        changeF2 = false;
        //Debug.Log("Miss");
        yield return new WaitForSeconds(0.1f);
        //Debug.Log("CountStart");
        Debug.Log("Start");
        while (true)
        {
           
            if (Mathf.Approximately(time, 0.0f))
            {
                ChangeImg(trueLife, falseLife);
                player.gameObject.GetComponent<Renderer>().material = translucent_material;
            }

            time += Time.deltaTime;

            if (time >= duration * timeDelay[0] && !changeF1)
            {
                ChangeImg(falseLife, trueLife);
                player.gameObject.GetComponent<Renderer>().material = default_material;

                changeF1 = true;
            }
            else if (time >= duration * timeDelay[1] && !changeF2)
            {
                ChangeImg(trueLife, falseLife);
                player.gameObject.GetComponent<Renderer>().material = translucent_material;

                changeF2 = true;
            }
            else if (time >= duration * timeDelay[2] && changeF2)
            {
                player.gameObject.GetComponent<Renderer>().material = default_material;

            }
         
            yield return null;
        }
       
    }

}
