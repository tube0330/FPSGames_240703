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
        HP = Mathf.Clamp(HP, 0, 100);   //�ڱ��ڽ�, �ּڰ�, �ִ�
        HPBar.fillAmount = (float)HP / (float)MAX_HP;

        if (HPBar.fillAmount <= 0.3)
            HPBar.color = Color.red;

        else if (HPBar.fillAmount <= 0.5)
            HPBar.color = Color.yellow;

        HPText.text = $"<color=#ff0000>HP</color>: {HP.ToString()}";
    }

    void PlayerDie()
    {
        BlackScreen.SetActive(true);    //���� ������Ʈ �ѱ� => ����ũ�� ��
        isPlayerDie = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        //��Ÿ��(�ǽð�)���� ENEMY��� �±׸� ���� ������Ʈ���� enemies��� ���ӿ�����Ʈ �迭�� ����

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SendMessage("PlayerDeath", SendMessageOptions.DontRequireReceiver);
            //�ٸ� ���ӿ�����Ʈ�� �ִ� �޼��� ȣ���ϴ� ����� ���� �޼���
        }

        Invoke("MoveToNextScene", 2.0f);    //���ڸ��� 3.0�� �Ŀ� MoveToNextScene �Լ� ȣ��
    }

    void MoveToNextScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
