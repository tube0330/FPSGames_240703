using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;  //Scene ���� ����� ����Ѵ�. �׸��� �� �ڴ� ������ ���
using UnityEngine.UI;

public class UIManager : MonoBehaviour//(Mono~: �� Ŭ������ ����Ƽ ������ �ִ°͵��� ����Ѵ�.)
{
    public Text FinalKillScore;
    private void Start()
    {
        /*Scene���� Scene���� �Ѿ�� ���콺 Ŀ�� �Ⱥ��̴ϱ�*/
        Cursor.visible = true;  //���콺 Ŀ�� ���̰�
        Cursor.lockState = CursorLockMode.None;   //���콺 Ŀ���� �ʿ����� �� ���

        /*Cursor.visible = false;                   //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 ����*/

        FinalKillScore = GameObject.Find("Text_FinalKillText").GetComponent<Text>();
        FinalKillScore.text = $"Kill Score {GameManager.KillCount.ToString()}";
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR    //��ó����: ������ �� �̸� ����� �������ִ�.
        UnityEditor.EditorApplication.isPlaying = false;
        //����Ƽ���� �������� ���¿� ����

#else   //���忡�� ����
    Application.Quit();

#endif
    }
}
