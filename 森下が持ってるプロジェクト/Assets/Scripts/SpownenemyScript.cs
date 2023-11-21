using System.Collections.Generic;
using UnityEngine;

public class SpownenemyScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成するGameObject")]
    private GameObject createPrefab;
    [SerializeField]
    [Tooltip("生成する場所")]
    private Transform[] SpownPoints;
    [SerializeField]
    [Tooltip("生成する間隔")]
    private float SpownTime = 5;


    int enemyCount = 0;

    // 経過時間
    private float time = 0f;
    // リスポーン位置をプレイヤーと被っていない場所からランダムに決めるためのリスト
    List<Transform> transforms = new List<Transform>();

    // Update is called once per frame
    void Update()
    {

        if (time >= SpownTime)
        {
            transforms.Clear();
            for (int i = 0; i < SpownPoints.Length; i++)
            {
                if (!SpownPoints[i].GetComponent<PlayerTriggerCheck>().isOn)
                {
                    transforms.Add(SpownPoints[i]);
                }
            }


            for (int i = 0; i < 2; i++)
            {
                // 生成できる場所がなかったら終了
                if (transforms.Count == 0)
                    break;

                int rand = Random.Range(0, transforms.Count);

                Vector3 SpownPos = transforms[rand].position;

                enemyCount++;

                if (enemyCount < 11)
                {
                    // GameObjectを上記で決まったランダムな場所に生成
                    Instantiate(createPrefab, SpownPos, createPrefab.transform.rotation);
                    // 同時に同じ場所から生成しない
                    transforms.RemoveAt(rand);
                }
            }

            time = 0f;
        }
        time = time + Time.deltaTime;
        //Debug.Log(time);
    }
}