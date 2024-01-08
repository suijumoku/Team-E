//���V
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] GameObject player;
    //Animator playerAnimator;
    //AnimatorStateInfo playerStateInfo;

    // �O�t���[���ŎՕ����Ƃ��Ĉ����Ă����Q�[���I�u�W�F�N�g���i�[�����
    public GameObject[] prevRaycast;
    public List<GameObject> raycastHitsList_ = new List<GameObject>();

    float maxDistance; //ray���΂��ő勗���B
    Vector3 _difference;

    // Start is called before the first frame update
    void Start()
    {
        _difference = (player.transform.position - this.transform.position);
        maxDistance = _difference.magnitude;    //��̃I�u�W�F�N�g�Ԃ̃x�N�g���̒��������߂Ċi�[�ARay���΂��ő勗���𐧌�
                                                //magnitude�͕������̌v�Z�Œ��������߂�֐�
    }

    // Update is called once per frame
    void Update()
    {
        //playerAnimator = player.GetComponent<Animator>();
        //playerStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        //if (!playerStateInfo.IsName("Jumping")) //�W�����v����ray���΂��Ȃ��@���{�̉����ɂȂ��ĂȂ�
        //{
        //    Raycast();
        //}        
        Raycast();
    }

    void FixedUpdate()      //cinemachine camera��Update Method��fixed updata�����炱��������
    {
        
    }
    void Raycast()
    {
        //��̃I�u�W�F�N�g�Ԃ̃x�N�g�����擾
         _difference = (player.transform.position - this.transform.position);   
         //.normalized�x�N�g���̐��K�����s��
        Vector3 _direction = _difference.normalized;
         //Debug.Log(_difference.sqrMagnitude);

        // Ray(�J�n�n�_,�@�i�ޕ���)
        Ray _ray = new Ray(this.transform.position, _direction);     

        // Ray���Փ˂����S�ẴR���C�_�[�̏��𓾂�
        RaycastHit[] rayCastHits = Physics.RaycastAll(_ray, maxDistance);

        // �O�t���[���ŎՕ����������S�Ă�GameObject��ێ�
        prevRaycast = raycastHitsList_.ToArray();
        raycastHitsList_.Clear();

        foreach (RaycastHit hit in rayCastHits)
        {
            Semitransparent semiTransparent = hit.collider.GetComponent<Semitransparent>();
            if (hit.collider.tag == "Wall")
            {
                //�ǂɂ܂������߂荞��ł����������Ă��܂�
                
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
