using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
/* 싱글톤 기법
 * 게임 매니저는 게임 전체를 컨트롤해야 하므로 접근이 쉬워야 한다.
 * static 변수를 만든 후 이 변수가 대표해서 게임 매니저에 접근하게 해야한다.
 * 무분별한 객체 생성을 막고 하나만 생성하는 기법이다.
 */

public class GameManager : MonoBehaviour
{
    // enemy가 태어나는 로직과 더불어 게임 전체를 아우르는 기능. 즉, 조절하는 클래스
    // 1. 적 prefab
    // 2. 태어날 위치들
    // 3. 시간 간격
    // 4. 몇 마리 태어날지

    public static GameManager Instance;
    public GameObject Enemies1;
    public GameObject Enemies2;
    public GameObject Enemies3;
    public Transform[] Points;
    private float timePrev;
    private int maxCount = 10;
    string EnemyTag = "ENEMY";
    public Text KillText;
    public static int KillCount = 0;
    void Start()
    {
        Instance = this/*자기자신 = 게임매니저클래스*/;    //객체 생성
        //게임 매니저의 public이라고 선언된 변수나 메서드는 다 접근 가능
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        //자기 자신을 포함해서 그 하위 오브젝트의 트랜스폼들을 Points 배열에 다 넣음
    }

    void Update()
    {
        timePrev += Time.deltaTime;

        int enemyCount = GameObject.FindGameObjectsWithTag(EnemyTag).Length;

        if (timePrev >= 3f)
        {
            if (enemyCount < maxCount)
            {
                CreateEnemies();
                
            }
            timePrev = 0;
        }
    }

    void CreateEnemies()
    {
        int pos = Random.Range(1, Points.Length);
        int i = Random.Range(1, 3);
        print(i.ToString());
        if (i == 1)
            Instantiate(Enemies1, Points[pos].position, Points[pos].rotation);
        if (i == 2)
            Instantiate(Enemies2, Points[pos].position, Points[pos].rotation);
        else if(i == 3)
            Instantiate(Enemies3, Points[pos].position, Points[pos].rotation);

    }

    public void KillScore(int score)
    {
        KillCount += score;
        KillText.text = $"KILL <color=#ff0000>{KillCount.ToString()}</color>";
    }
}
