using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour
{
    private Transform tr;   //�ڱ��ڽ� Ʈ������
    private Ray ray;        //����
    private RaycastHit hit; //�������� ����ü
    //RayCastHit ���� ����ü ������Ʈ�� ������ �¾Ҵ��� �Ǻ�
    private float dist = 20f;
    public CrossHair C_Hair;
    void Start()
    {
        C_Hair = GameObject.Find("Canvas_UI").transform.GetChild(3).GetComponent<CrossHair>();
        tr = GetComponent<Transform>(); //�� ��ũ��Ʈ�� ���ִ� ī�޶� ��ġ ������
    }

    void Update()
    {
        ray = new Ray(tr.position, tr.forward);
        // ���� �Ҵ� ���ڸ��� ��ġ�� ���� ����

        Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);   //���� * �Ÿ� = Velocity
        //Sceneȭ�鿡�� ������ ���̴��� ������ �׽�Ʈ
        //ray.origin: ��ġ

        if (Physics.Raycast(ray/*��ġ�� ������ ������*/, out hit, dist, 1 << 6 | 1 << 7 | 1 << 8 /*6: ����, 7:����, 8��° ���̾ ���̷��� ����*/))
        {   //������ ����� �� ���� �¾Ҵٸ�
            C_Hair.isGaze = true;
        }

        else
        {   //������ ����� �� ���� ���� �ʾҴٸ�
            C_Hair.isGaze = false;

        }
    }
}
