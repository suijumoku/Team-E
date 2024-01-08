using System.Collections.Generic;
using UnityEngine;

public class SpownenemyScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成するGameObject")]
    private GameObject[] createPrefab;
    [SerializeField]
    [Tooltip("生成する場所")]
    private Transform[] SpownPoints;
    [SerializeField]
    [Tooltip("生成する間隔")]
    private float SpownTime = 5;
    [SerializeField]
    [Tooltip("敵の出現上限")]
    private int enemyNumlimit = 10;
    [SerializeField]
    [Tooltip("敵が一度に出現する数")]
    private int enemyOnceSpown = 2;
    [SerializeField]
    [Tooltip("敵のカウントを別々にする")]
    private bool anotherEnemyCount=false;
    [SerializeField]
    [Tooltip("敵が出現したときのSE")]
    private AudioClip spownSE;


    [SerializeField]
    [Tooltip("生成したEnemyタグのGameObject")]
    private GameObject[] createdEnemyTagPrefab;
    private int enemyCount = 0;

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

            if(transforms.Count!=0)
            {
                for (int i = 0; i < enemyOnceSpown; i++)
                {
                    // 生成できる場所がなかったら終了
                    if (transforms.Count == 0)
                        break;

                    if(!anotherEnemyCount)
                    enemyCount = EnemyCountCheck("Enemy");

                    if (enemyCount < enemyNumlimit)
                    {
                        int rand = Random.Range(0, transforms.Count);
                        int darumaRand = Random.Range(0, createPrefab.Length);

                        Vector3 SpownPos = transforms[rand].position;

                        // GameObjectを上記で決まったランダムな場所に生成
                        Instantiate(createPrefab[darumaRand], SpownPos, createPrefab[darumaRand].transform.rotation);
                        GameManager.instance.PlaySE(spownSE);
                        // 同時に同じ場所から生成しない
                        transforms.RemoveAt(rand);

                        enemyCount++;

                    }
                }
                print("生成");
                print(transforms.Count);
            }
            else
            {
                print("生成不可");
            }

            time = 0f;
        }
        time = time + Time.deltaTime;
        if(!anotherEnemyCount)
        enemyCount = EnemyCountCheck("Enemy");

        //Debug.Log(time);
    }

    int EnemyCountCheck(string tagname)
    {
        createdEnemyTagPrefab = GameObject.FindGameObjectsWithTag(tagname);
        return createdEnemyTagPrefab.Length;
    }
}