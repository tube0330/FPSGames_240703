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
        targetCam = GetComponent<Transform>();  //얘는 자기 자신 이 스크립트는 좀비에 있으니까
    }

    void Update()
    {
        targetCam.LookAt(mainCam);  //캔버스가 메인카메라를 쳐다본다.
    }
}
