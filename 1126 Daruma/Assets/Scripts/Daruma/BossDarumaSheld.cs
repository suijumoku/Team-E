using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDarumaSheld : MonoBehaviour
{
    const int SHELD_NUM = 4;

    [Header("盾に使うオブジェクト")][SerializeField] GameObject SourceSheldObject;
    [Header("盾を出す位置")][SerializeField] Transform[] SheldObjectPosition = new Transform[SHELD_NUM];
    [Header("盾のリスト")][SerializeField] List<GameObject> SheldList;


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
