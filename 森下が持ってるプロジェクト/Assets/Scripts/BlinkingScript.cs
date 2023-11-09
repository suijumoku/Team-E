using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingScript : MonoBehaviour
{
    [SerializeField] GameObject player = default!;
    [Header("表示場所")]
    [SerializeField] Image[] lifeImage = default!;
    [Header("通常時画像")]
    [SerializeField] Sprite truelife = default!;
    [Header("ダメージ時画像")]
    [SerializeField] Sprite falselife = default!;
    [Header("通常時マテリアル")]
    [SerializeField] Material trueMaterial = default!;
    [Header("ダメージ時マテリアル")]
    [SerializeField] Material falseMaterial = default!;
    [Header("ダメージ時の表示間隔")]
    [SerializeField] float[] duration = default!;

    [SerializeField] MainGameManager _MainGameManager = default!;

    int life = 3;


    void Awake()
    {
        //最初に全てのLife画像をtrueに
        foreach (Image t in lifeImage)
        {
            t.enabled = truelife;
        }
            int life = 3;
    }

    //number：表示画像番号 x：偶数奇数判定
    void lifeChange(int number, int x)
    {
        if (x % 2 == 0)
        {
            lifeImage[number].sprite = falselife;
            player.gameObject.GetComponent<Renderer>().material = falseMaterial;
        }
        else
        {
            lifeImage[number].sprite = truelife;
            player.gameObject.GetComponent<Renderer>().material = trueMaterial;
        }
    }

    public IEnumerator DamageIndication(int i)
    {
        yield return new WaitForSeconds(0.15f);
        //WaitForSecondsでそれぞれ待機してからLifeChangeを行う
        for (int j = 0; j < duration.Length; j++)
        {
            lifeChange(i, j);
            yield return new WaitForSeconds(duration[j]);
        }

        //最後は減らさなければならないのでfalseに
        lifeImage[i].sprite = falselife;

        //プレイヤーのマテリアルを通常に。ハートのより点滅の回数が増えてしまう
       // yield return new WaitForSeconds(0.1f);
        player.gameObject.GetComponent<Renderer>().material = trueMaterial;
        life--;
        if (life <= 0)
        {
            _MainGameManager.isDefeat = true;
        }
        yield return null;
    }

}
