//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Respawn : MonoBehaviour
//{
//    [SerializeField]
//    [Tooltip("�v���C���[�̃v���n�u��ݒ�")]
//    private GameObject playerPrefab;

//    void Update()
//    {
//        // �ݒ肵��playerPrefab�Ɠ������O��GameObject��T���Ď擾
//        GameObject playerObj = GameObject.Find(playerPrefab.name);

//        // playerObj�����݂��Ă��Ȃ��ꍇ
//        if (playerObj == null)
//        {
//            // playerPrefab����V����GameObject���쐬
//            GameObject newPlayerObj = Instantiate(playerPrefab);

//            // �V�����쐬����GameObject�̖��O���Đݒ�(�����"PlayerSphere"�ƂȂ�)
//            newPlayerObj.name = playerPrefab.name;
//            // �������Ŗ��O���Đݒ肵�Ȃ��ꍇ�A�����Ō��܂閼�O�́A"PlayerSphere(Clone)"�ƂȂ邽��
//            //   13�s�ڂŒT���Ă���"PlayerSphere"���i���Ɍ�����Ȃ����ƂɂȂ�Aplayer�������ɐ��Y�����
//            //   �ǂ��������Ƃ��́A22�s�ڂ��R�����g�A�E�g���ăQ�[�������s����΂킩��܂��B
//        }
//    }
//}
