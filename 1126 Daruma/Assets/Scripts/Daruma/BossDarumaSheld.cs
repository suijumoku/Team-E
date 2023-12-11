using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDarumaSheld : MonoBehaviour
{
    const int SHELD_NUM = 4;

    [Header("���Ɏg���I�u�W�F�N�g")][SerializeField] GameObject SourceSheldObject;
    [Header("�����o���ʒu")][SerializeField] Transform[] SheldObjectPosition = new Transform[SHELD_NUM];
    [Header("���̃��X�g")][SerializeField] List<GameObject> SheldList;


    private void Start()
    {
        int rand = Random.Range(0, SHELD_NUM);
        print("rand:" + rand);

        for (int i = 0; i < SHELD_NUM; i++)
        {
            if (i != rand)
            {
                print("spown:" + i);
                float rotation = 90 * i;
                SheldList.Add((GameObject)Instantiate(SourceSheldObject, SheldObjectPosition[i].position, Quaternion.Euler(0,rotation,0)));
                print("Listlength:" + SheldList.Count);
                SheldList[SheldList.Count - 1].gameObject.transform.parent = this.transform;

            }
        }
    }


    private void Update()
    {
        if (SheldList.Count == 0)
            Destroy(this.gameObject);
    }
}
