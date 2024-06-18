using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IngrediantGenerator : MonoBehaviour
{
    public static IngrediantGenerator instance;
    public static IngrediantGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<IngrediantGenerator>();

                if (instance == null)
                {
                    Debug.Log("No Singletone instance");
                    Debug.Log("이거 나오면 망한거지 뭐...");
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        //게임매니저 인스턴스 생성 대기 후 변수 매칭
        StartCoroutine(WaitForGameManager());
    }

    void Start()
    {
        // 재료 생성 코루틴 시작
        StartCoroutine(SpawnMaterialRoutine());
    }
    // 재료 프리팹
    public GameObject[] materialPrefab;

    //생성할 재료 인덱스
    int index = 0;
    // 재료 생성 시간 간격
    public float spawnInterval;

    // 맵 내 재료 최대 갯수
    public int maxMaterials;

    // 고정된 좌표들
    public Vector3[] spawnPoints;

    //맵에 존재하는 재료 갯수 카운터. GameManager에 선언된 배열의 참조를 복사해와서 사용합니다.
    public int[] mapIngredCounter;

    //재료 생성 좌표 선택여부 리스트. GameManager에 선언된 배열의 참조를 복사해와서 사용합니다.
    public bool[] isSpawnPointSelected;

    // 현재 맵 내 존재하는 재료 리스트
    private List<GameObject> existingIngreds = new List<GameObject>();//GameManager로 넘겨야할듯.

    private IEnumerator WaitForGameManager()
    {
        yield return new WaitUntil(() => GameManager.instance);//게임매니저 생성 대기

        mapIngredCounter = GameManager.instance.mapIngredCount;
        isSpawnPointSelected = GameManager.instance.isSpawnPointSelected;
    }
        IEnumerator SpawnMaterialRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            TrySpawnMaterial();
        }
    }

    void TrySpawnMaterial()
    {
        Vector3 spawnPoint;
        // 현재 맵 내 재료 갯수 체크
        if (existingIngreds.Count >= maxMaterials)
        {
            return;
        }

        // 빈 좌표들 찾기. 아래 메서드는 isSpawnPointSelected의 원소가 모두 true이면 true를 반환.
        bool allTrue = isSpawnPointSelected.All(b => b);

        // 빈 좌표가 없으면 생성하지 않음
        if (allTrue)
        {
            return;
        }
        while (true)// 랜덤하게 빈 좌표 선택.
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length-1);
            if (isSpawnPointSelected[spawnPointIndex])
            {
                continue;
            }
            else if (isSpawnPointSelected[spawnPointIndex] == false)
            {
                spawnPoint = spawnPoints[spawnPointIndex];
                isSpawnPointSelected[spawnPointIndex] = true;
                break;
            }
            else
            {
                Debug.Log("재료생성 오류 발생. 재시도합니다.");
            }
        }

        while (true)// 생성할 재료 선택. 조건 불만족시 재생성
        {
            index = Random.Range(1, 7);
            if (index < 4)//빵, 토마토, 치즈의 경우
            {
                if (mapIngredCounter[index] < 10)//각 재료별 10개까지
                {
                    break;
                }//10개 넘는경우 재생성
            }
            else if (index > 3)//치즈,햄,또띠아의 경우
            {
                if (mapIngredCounter[index] < 5)//각 재료별 5개까지
                {
                    break;
                }//5개 넘는경우 재생성
            }
        }

        GameObject newMaterial = Instantiate(materialPrefab[index], spawnPoint, Quaternion.identity);
        //재료 생성

        existingIngreds.Add(newMaterial);//맵에 존재하는 재료 배열 변경
        mapIngredCounter[index]++; //맵에 존재하는 재료 카운터 변경

    }
}
