using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IngrediantGenerator : MonoBehaviour
{
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
    public int[] mapIngredCounter = GameManager.instance.mapIngredCount;

    // 현재 맵 내 존재하는 재료 리스트
    private List<GameObject> existingIngreds = new List<GameObject>();

    void Start()
    {
        // 재료 생성 코루틴 시작
        StartCoroutine(SpawnMaterialRoutine());
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
        // 현재 맵 내 재료 갯수 체크
        if (existingIngreds.Count >= maxMaterials)
        {
            return;
        }

        // 빈 좌표들 찾기
        List<Vector3> availableSpawnPoints = new List<Vector3>();
        foreach (var point in spawnPoints)
        {
            bool isOccupied = false;
            foreach (var material in existingIngreds)
            {
                if (material.transform.position == point)
                {
                    isOccupied = true;
                    break;
                }
            }
            if (!isOccupied)
            {
                availableSpawnPoints.Add(point);
            }
        }

        // 빈 좌표가 없으면 생성하지 않음
        if (availableSpawnPoints.Count == 0)
        {
            return;
        }

        Vector3 spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
        // 랜덤하게 빈 좌표 선택

        while (true)// 생성할 재료 선택. 조건 불만족시 재생성
        {
            index = Random.Range(1, 6);
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
