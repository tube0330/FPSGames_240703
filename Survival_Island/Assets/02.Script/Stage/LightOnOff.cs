using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class LightOnOff : MonoBehaviour
{
    public Light StairLight;
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {

    }

    /*is Trigger ���� �� ��� �ϸ鼭 �浹 �����ϴ� �Լ�
    ������ ȣ���ϱ� ������ �ݹ� �Լ���� ��*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StairLight.enabled = true;
            source.PlayOneShot(clip, 1.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StairLight.enabled = false;
            source.PlayOneShot(clip, 1.0f);
        }
    }
}
