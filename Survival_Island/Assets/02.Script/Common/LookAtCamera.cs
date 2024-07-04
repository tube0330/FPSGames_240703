using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform mainCam;
    public Transform targetCam;
    void Start()
    {
        mainCam = Camera.main.transform;
        targetCam = GetComponent<Transform>();  //��� �ڱ� �ڽ� �� ��ũ��Ʈ�� ���� �����ϱ�
    }

    void Update()
    {
        targetCam.LookAt(mainCam);  //ĵ������ ����ī�޶� �Ĵٺ���.
    }
}
