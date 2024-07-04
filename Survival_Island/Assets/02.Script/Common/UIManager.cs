using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;  //Scene 관련 기능을 사용한다. 그리고 그 뒤는 생략을 명시
using UnityEngine.UI;

public class UIManager : MonoBehaviour//(Mono~: 이 클래스가 유니티 엔진에 있는것들을 상속한다.)
{
    public Text FinalKillScore;
    private void Start()
    {
        /*Scene에서 Scene으로 넘어가면 마우스 커서 안보이니까*/
        Cursor.visible = true;  //마우스 커서 보이게
        Cursor.lockState = CursorLockMode.None;   //마우스 커서가 필요해질 때 사용

        /*Cursor.visible = false;                   //마우스 커서 숨기고
        Cursor.lockState = CursorLockMode.Locked;   //마우스 고정*/

        FinalKillScore = GameObject.Find("Text_FinalKillText").GetComponent<Text>();
        FinalKillScore.text = $"Kill Score {GameManager.KillCount.ToString()}";
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR    //전처리기: 컴파일 전 미리 기능이 정해져있다.
        UnityEditor.EditorApplication.isPlaying = false;
        //유니티에서 편집중인 상태에 종료

#else   //빌드에서 종료
    Application.Quit();

#endif
    }
}
