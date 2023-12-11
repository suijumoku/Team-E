//古澤
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private Transform player;

    // 前フレームで遮蔽物として扱われていたゲームオブジェクトが格納される
    public GameObject[] prevRaycast;
    public List<GameObject> raycastHitsList_ = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Raycast();
    }

    void FixedUpdate()      //cinemachine cameraのUpdate Methodがfixed updataだからこっちかも
    {
        
    }
    void Raycast()
    {
        //二つのオブジェクト間のベクトルを取得
        Vector3 _difference = (player.transform.position - this.transform.position);
        //.normalizedベクトルの正規化を行う
        Vector3 _direction = _difference.normalized;
        // Ray(開始地点,　進む方向)
        Ray _ray = new Ray(this.transform.position, _direction);

        // Rayが衝突した全てのコライダーの情報を得る
        RaycastHit[] rayCastHits = Physics.RaycastAll(_ray);

        // 前フレームで遮蔽物だった全てのGameObjectを保持
        prevRaycast = raycastHitsList_.ToArray();
        raycastHitsList_.Clear();

        foreach (RaycastHit hit in rayCastHits)
        {
            Semitransparent semiTransparent = hit.collider.GetComponent<Semitransparent>();
            if (hit.collider.tag == "Wall")
            {
                //半透明化
                semiTransparent.ClearMaterialInvoke();
                //次のフレームで使いたいから、不透明にしたオブジェクトを追加する
                raycastHitsList_.Add(hit.collider.gameObject);
            }
        }

        //このforeach文はExceptメソッドを使って、prevRaycastからraycastHitsList_の要素を除外した結果を走査
        //前フレームで遮蔽物だったもの以外が万が一入っていた時の保険？
        foreach (GameObject _gameObject in prevRaycast.Except<GameObject>(raycastHitsList_))
        {
            Semitransparent noSemiTransparent = _gameObject.GetComponent<Semitransparent>();
            // 遮蔽物でなくなったGameObjectを通常に戻す

            if (_gameObject != null)
            {
                noSemiTransparent.NotClearMaterialInvoke();
            }
        }
    }
}
