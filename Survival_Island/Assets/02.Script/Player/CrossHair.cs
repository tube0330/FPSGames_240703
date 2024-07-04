using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    Transform tr;
    Image crossHair_img;
    float startTime;    //ũ�ν��� Ŀ���� ������ �ð��� ����
    float duration = 0.25f;  //ũ�ν��� Ŀ���� �ӵ�
    float minSize = 0.7f;
    float maxSize = 1.2f;
    Color originColor = new Color(1f, 1f, 1f, 0.8f);    //R,G,B,����
    //ũ�ν������ �ʱ����
    Color gazeColor = Color.red;    //���� �������� �� ����
    public bool isGaze = false;     //������������ �Ǵ�
    void Start()
    {
        tr = transform;
        crossHair_img = GetComponent<Image>();
        startTime = Time.time;  //���Ž÷� ����� ����
        tr.localScale/*�θ�(ĵ����)ũ�� ���� �� �� ũ�� ���ڴ�*/ = Vector3.one/*x, y, z�� ������ ũ�⸦ ������ ����*/ * minSize;  //ũ�ν������ ó�� ũ��
    }

    void Update()
    {
        if (isGaze)  //Ray�� �� ������Ʈ�� �����ߴٸ�
        {
            float time = (Time.time - startTime) / duration;
            //���� �ð�         /  0.25
            tr.localScale = Vector3.one/*x, y, z�� ������ ũ��� ����*/ * Mathf/*���� �Լ�*/.Lerp/*Linear Interpolate: ��������(min->max���� �ε巴�� �����ϰڴ�.)*/(minSize, maxSize, time);

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
