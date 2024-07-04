using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPSDamage : MonoBehaviour
{

    public Image HPBar;
    public Text HPText;
    public int HP = 0;
    public int MAX_HP = 100;
    public string attackTag_sk = "ATTACK_SK";
    public string attackTag_mon = "ATTACK_MON";
    public string attackTag_zom = "ATTACK_ZOM";
    public bool isPlayerDie = false;
    public GameObject BlackScreen;
    void Start()
    {
        BlackScreen = GameObject.Find("Canvas_UI").transform.GetChild(5).gameObject;
        //BlackScreen = GameObject.Find("BlackScreen").gameObject;
        HP = MAX_HP;
        HPBar.color = Color.green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(attackTag_sk))
        {
            HP_Info();

            if (HP <= 0)
                PlayerDie();
        }

        if (other.gameObject.CompareTag(attackTag_mon))
        {
            HP_Info();

            if (HP <= 0)
                PlayerDie();
        }

        if (other.gameObject.CompareTag(attackTag_zom))
        {
            HP_Info();

            if (HP <= 0)
                PlayerDie();
        }
    }

    private void HP_Info()
    {
        HP -= 5;
        HP = Mathf.Clamp(HP, 0, 100);   //자기자신, 최솟값, 최댓값
        HPBar.fillAmount = (float)HP / (float)MAX_HP;

        if (HPBar.fillAmount <= 0.3)
            HPBar.color = Color.red;

        else if (HPBar.fillAmount <= 0.5)
            HPBar.color = Color.yellow;

        HPText.text = $"<color=#ff0000>HP</color>: {HP.ToString()}";
    }

    void PlayerDie()
    {
        BlackScreen.SetActive(true);    //꺼진 오브젝트 켜기 => 블랙스크린 켬
        isPlayerDie = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        //런타임(실시간)에서 ENEMY라는 태그를 가진 오브젝트들을 enemies라는 게임오브젝트 배열에 저장

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SendMessage("PlayerDeath", SendMessageOptions.DontRequireReceiver);
            //다른 게임오브젝트에 있는 메서드 호출하는 기능을 가진 메서드
        }

        Invoke("MoveToNextScene", 2.0f);    //죽자마자 3.0초 후에 MoveToNextScene 함수 호출
    }

    void MoveToNextScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
