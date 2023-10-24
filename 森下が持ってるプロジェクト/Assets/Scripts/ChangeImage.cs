////���V
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    [SerializeField] Material default_material;
    [SerializeField] Material translucent_material;
    [SerializeField] GameObject player;
    //�����T�C�Y�̉摜���d�˂ĕ\����ON,OFF�Ő؂�ւ���
    [SerializeField] Image trueImage, falseImage;
    
    [SerializeField] float duration = 0.2f;  //�_�ł̊��o(�v����)

    private ChangeImage instance;
            
    private float time = 0.0f;             // �o�ߎ��Ԃ��i�[����ϐ�    
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
        player = GameObject.FindWithTag("Player");
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
                    player.gameObject.GetComponent<Renderer>().material = translucent_material;
                }
                time += Time.deltaTime;
                if (time >= duration && !changeF1)
                {
                    ChangeImg(falseImage, trueImage);
                    player.gameObject.GetComponent<Renderer>().material = default_material;

                    changeF1 = true;
                }
                else if (time >= duration * 2 && !changeF2)
                {
                    ChangeImg(trueImage, falseImage);
                    player.gameObject.GetComponent<Renderer>().material = translucent_material;

                    changeF2 = true;
                }
                else if (time >= duration * 3 && changeF2)
                {
                    player.gameObject.GetComponent<Renderer>().material = default_material;
                }

                yield return null;
            }       

              
    }
}
