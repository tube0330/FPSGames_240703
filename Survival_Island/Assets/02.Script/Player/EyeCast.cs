using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour
{
    private Transform tr;   //자기자신 트랜스폼
    private Ray ray;        //광선
    private RaycastHit hit; //광선관련 구조체
    //RayCastHit 광선 구조체 오브젝트가 광선에 맞았는지 판별
    private float dist = 20f;
    public CrossHair C_Hair;
    void Start()
    {
        C_Hair = GameObject.Find("Canvas_UI").transform.GetChild(3).GetComponent<CrossHair>();
        tr = GetComponent<Transform>(); //이 스크립트가 들어가있는 카메라 위치 가져옴
    }

    void Update()
    {
        ray = new Ray(tr.position, tr.forward);
        // 동적 할당 하자마자 위치와 방향 지정

        Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);   //방향 * 거리 = Velocity
        //Scene화면에서 광선이 보이는지 개발자 테스트
        //ray.origin: 위치

        if (Physics.Raycast(ray/*위치와 방향을 가져옴*/, out hit, dist, 1 << 6 | 1 << 7 | 1 << 8 /*6: 좀비, 7:몬스터, 8번째 레이어에 스켈레톤 있음*/))
        {   //광선을 쏘았을 때 적이 맞았다면
            C_Hair.isGaze = true;
        }

        else
        {   //광선을 쏘았을 때 적이 맞지 않았다면
            C_Hair.isGaze = false;

        }
    }
}
