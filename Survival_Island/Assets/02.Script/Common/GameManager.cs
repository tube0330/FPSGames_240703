using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
/* �̱��� ���
 * ���� �Ŵ����� ���� ��ü�� ��Ʈ���ؾ� �ϹǷ� ������ ������ �Ѵ�.
 * static ������ ���� �� �� ������ ��ǥ�ؼ� ���� �Ŵ����� �����ϰ� �ؾ��Ѵ�.
 * ���к��� ��ü ������ ���� �ϳ��� �����ϴ� ����̴�.
 */

public class GameManager : MonoBehaviour
{
    // enemy�� �¾�� ������ ���Ҿ� ���� ��ü�� �ƿ츣�� ���. ��, �����ϴ� Ŭ����
    // 1. �� prefab
    // 2. �¾ ��ġ��
    // 3. �ð� ����
    // 4. �� ���� �¾��

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
        Instance = this/*�ڱ��ڽ� = ���ӸŴ���Ŭ����*/;    //��ü ����
        //���� �Ŵ����� public�̶�� ����� ������ �޼���� �� ���� ����
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        //�ڱ� �ڽ��� �����ؼ� �� ���� ������Ʈ�� Ʈ���������� Points �迭�� �� ����
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
