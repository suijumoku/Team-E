//���V
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private Transform player;

    // �O�t���[���ŎՕ����Ƃ��Ĉ����Ă����Q�[���I�u�W�F�N�g���i�[�����
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

    void FixedUpdate()      //cinemachine camera��Update Method��fixed updata�����炱��������
    {
        
    }
    void Raycast()
    {
        //��̃I�u�W�F�N�g�Ԃ̃x�N�g�����擾
        Vector3 _difference = (player.transform.position - this.transform.position);
        //.normalized�x�N�g���̐��K�����s��
        Vector3 _direction = _difference.normalized;
        // Ray(�J�n�n�_,�@�i�ޕ���)
        Ray _ray = new Ray(this.transform.position, _direction);

        // Ray���Փ˂����S�ẴR���C�_�[�̏��𓾂�
        RaycastHit[] rayCastHits = Physics.RaycastAll(_ray);

        // �O�t���[���ŎՕ����������S�Ă�GameObject��ێ�
        prevRaycast = raycastHitsList_.ToArray();
        raycastHitsList_.Clear();

        foreach (RaycastHit hit in rayCastHits)
        {
            Semitransparent semiTransparent = hit.collider.GetComponent<Semitransparent>();
            if (hit.collider.tag == "Wall")
            {
                //��������
                semiTransparent.ClearMaterialInvoke();
                //���̃t���[���Ŏg����������A�s�����ɂ����I�u�W�F�N�g��ǉ�����
                raycastHitsList_.Add(hit.collider.gameObject);
            }
        }

        //����foreach����Except���\�b�h���g���āAprevRaycast����raycastHitsList_�̗v�f�����O�������ʂ𑖍�
        //�O�t���[���ŎՕ������������̈ȊO������������Ă������̕ی��H
        foreach (GameObject _gameObject in prevRaycast.Except<GameObject>(raycastHitsList_))
        {
            Semitransparent noSemiTransparent = _gameObject.GetComponent<Semitransparent>();
            // �Օ����łȂ��Ȃ���GameObject��ʏ�ɖ߂�

            if (_gameObject != null)
            {
                noSemiTransparent.NotClearMaterialInvoke();
            }
        }
    }
}
