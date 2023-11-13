using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpownenemyScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成するGameObject")]
    private GameObject createPrefab;
    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Transform rangeA;
    [SerializeField]
    [Tooltip("生成する範囲B")]
    private Transform rangeB;

    int a;

    // 経過時間
    private float time = 0f;

    public void Hit()
    {
        a = a - 1;
    }

    // Update is called once per frame
    void Update()
    {
        // 前フレームからの時間を加算していく

        // 約1秒置きにランダムに生成されるようにする。
        if (time == 0f || time > 10f)
        {
            for (int i = 0; i < 2; i++)
            {
                a++;
                // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
                float x = Random.Range(rangeA.position.x, rangeB.position.x);
                // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
                float y = Random.Range(rangeA.position.y, rangeB.position.y);
                // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
                float z = Random.Range(rangeA.position.z, rangeB.position.z);

                if (a < 11)
                {
                    // GameObjectを上記で決まったランダムな場所に生成
                    Instantiate(createPrefab, new Vector3(x, y, z), createPrefab.transform.rotation);
                }

            }

            time = 0f;
        }
        time = time + Time.deltaTime;
        //Debug.Log(time);
    }
}