//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Respawn : MonoBehaviour
//{
//    [SerializeField]
//    [Tooltip("プレイヤーのプレハブを設定")]
//    private GameObject playerPrefab;

//    void Update()
//    {
//        // 設定したplayerPrefabと同じ名前のGameObjectを探して取得
//        GameObject playerObj = GameObject.Find(playerPrefab.name);

//        // playerObjが存在していない場合
//        if (playerObj == null)
//        {
//            // playerPrefabから新しくGameObjectを作成
//            GameObject newPlayerObj = Instantiate(playerPrefab);

//            // 新しく作成したGameObjectの名前を再設定(今回は"PlayerSphere"となる)
//            newPlayerObj.name = playerPrefab.name;
//            // ※ここで名前を再設定しない場合、自動で決まる名前は、"PlayerSphere(Clone)"となるため
//            //   13行目で探している"PlayerSphere"が永遠に見つからないことになり、playerが無限に生産される
//            //   どういうことかは、22行目をコメントアウトしてゲームを実行すればわかります。
//        }
//    }
//}
