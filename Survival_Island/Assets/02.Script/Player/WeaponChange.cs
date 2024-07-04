using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public SkinnedMeshRenderer spas12;
    public MeshRenderer[] AK47; //아래 3개씩 있어서 배열로 선언함
    public MeshRenderer[] M4A1;
    public Animation ComBatSg;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponChange1();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponChange2();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponChange3();
        }
    }

    private void WeaponChange3()
    {
        ComBatSg.Play("draw");

        foreach (MeshRenderer ak47 in AK47)
            ak47.enabled = false;

        foreach (MeshRenderer m4a1 in M4A1)
            m4a1.enabled = false;

        spas12.enabled = true;
    }

    private void WeaponChange2()
    {
        ComBatSg.Play("draw");

        for (int i = 0; i < AK47.Length; i++)
            AK47[i].enabled = false;

        spas12.enabled = false;

        for (int i = 0; i < M4A1.Length; i++)
            M4A1[i].enabled = true;
    }

    private void WeaponChange1()
    {
        ComBatSg.Play("draw");

        for (int i = 0; i < AK47.Length; i++)
            AK47[i].enabled = true;

        spas12.enabled = false;

        for (int i = 0; i < M4A1.Length; i++)
            M4A1[i].enabled = false;
    }
}
