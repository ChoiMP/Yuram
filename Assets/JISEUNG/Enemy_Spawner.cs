using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public static Enemy_Spawner instance;

    [Header("생성할 위치")]
    public GameObject[] spanwer_Point; // 생성할 위치 리스트
    [Header("생성할 적")]
    public GameObject[] spawn_Enemy;// 생성할 적 리스트
    public float spawn_Time; // 생성 주기

    public List<GameObject> spawned_Enemy; // 생성된 적 리스트
    
    public int spawn_Count; // 생성된 횟수

    [Header("플레이어 주위로 생성될 적")]
    public GameObject enemyPrefab; // 소환할 적 프리팹
  //  public Transform player; // 플레이어 Transform
    public int enemyCount = 30; // 소환할 적의 개수
    public float radius = 4f; // 적을 소환할 반지름

    public Vector2 cameraCenter;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        spawned_Enemy = new List<GameObject>();
        spawn_Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawn_Time <= 0)
        {
            spawn_Count++;
            if (spawn_Count == 5) // 특정 횟수만큼 소환했으면
            {
                SpawnEnemiesAroundPlayer();
            }

            int spawn_Count_ = Random.Range(10, 20);
            int spawn_Point_Num = 0; // 랜덤 생성 위치
            int spawn_Ememy_Num = 0; // 랜덤 생성 적

            for(int i = 0; i <= spawn_Count_; i++)
            {
                spawn_Point_Num = Random.Range(0, spanwer_Point.Length);
                spawn_Ememy_Num = Random.Range(0, spawn_Enemy.Length);
                int j;
                for (j = 0; j < spawned_Enemy.Count; j++)
                {
                    if (spawned_Enemy[j].activeSelf == false)
                    {
                        spawned_Enemy[j].transform.position = Return_RandomPosition(spanwer_Point[spawn_Point_Num]);
                        spawned_Enemy[j].SetActive(true);
                        break;
                    }
                }
                if(j == spawned_Enemy.Count)
                {
                    GameObject clone = Instantiate(spawn_Enemy[spawn_Ememy_Num], Return_RandomPosition(spanwer_Point[spawn_Point_Num]), Quaternion.identity);
                    spawned_Enemy.Add(clone);
                }
            }
            spawn_Time = 5f;
        }
        else
        {
            spawn_Time -= Time.deltaTime;
        }

        
    }

    Vector2 Return_RandomPosition(GameObject rangeObject)
    {
        Vector3 originPosition = rangeObject.transform.position;
        BoxCollider2D rangeCollider = rangeObject.GetComponent<BoxCollider2D>();

        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    void SpawnEnemiesAroundPlayer()
    {
        cameraCenter = Camera.main.transform.position;
        // 적을 원형으로 배치
        for (int i = 0; i < enemyCount; i++)
        {
            // 각도를 계산 (0 ~ 360도 사이로 분배)
            float angle = i * Mathf.PI * 2 / enemyCount;
            // 적의 위치 계산
            /* Vector2 spawnPosition = new Vector2(
                 player.position.x + Mathf.Cos(angle) * radius,
                 player.position.y + Mathf.Sin(angle) * radius*/
            Vector2 spawnPosition = new Vector2(
                cameraCenter.x + Mathf.Cos(angle) * radius,
                cameraCenter.y + Mathf.Sin(angle) * radius);

            // 적 생성
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
