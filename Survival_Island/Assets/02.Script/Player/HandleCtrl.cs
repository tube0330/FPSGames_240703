using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCtrl : MonoBehaviour
{
    public Animation ComBatSGAni;
    public Light FlashLight;
    public AudioClip FlashSound;
    public AudioSource A_source;
    public bool isRun = false;

    void Start()
    {
    }

    void Update()
    {
        GunCtrl();

        if (Input.GetKeyDown(KeyCode.F))
        {
            FlashLight.enabled = !FlashLight.enabled;
            A_source.PlayOneShot(FlashSound, 1.0f);
        }
    }
    private void GunCtrl()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            ComBatSGAni.Play("running");
            isRun = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ComBatSGAni.Play("runStop");
            isRun = false;
        }
    }
}
