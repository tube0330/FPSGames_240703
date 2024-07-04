using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    Transform tr;
    Image crossHair_img;
    float startTime;    //크로스헤어가 커지기 시작한 시간을 저장
    float duration = 0.25f;  //크로스헤어가 커지는 속도
    float minSize = 0.7f;
    float maxSize = 1.2f;
    Color originColor = new Color(1f, 1f, 1f, 0.8f);    //R,G,B,투명도
    //크로스헤어의 초기색상
    Color gazeColor = Color.red;    //적을 응시중일 때 색상
    public bool isGaze = false;     //응시중인지를 판단
    void Start()
    {
        tr = transform;
        crossHair_img = GetComponent<Image>();
        startTime = Time.time;  //과거시로 만들기 위해
        tr.localScale/*부모(캔버스)크기 말고 난 내 크기 쓰겠다*/ = Vector3.one/*x, y, z가 동일한 크기를 가지기 위해*/ * minSize;  //크로스헤어의 처음 크기
    }

    void Update()
    {
        if (isGaze)  //Ray가 적 오브젝트를 감지했다면
        {
            float time = (Time.time - startTime) / duration;
            //지난 시간         /  0.25
            tr.localScale = Vector3.one/*x, y, z가 동일한 크기로 만듦*/ * Mathf/*수학 함수*/.Lerp/*Linear Interpolate: 선형보간(min->max까지 부드럽게 동작하겠다.)*/(minSize, maxSize, time);

            crossHair_img.color = gazeColor;
        }

        else
        {
            tr.localScale = Vector3.one * minSize;
            crossHair_img.color = originColor;
            startTime = Time.time;
        }
    }
}
