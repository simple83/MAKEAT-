using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneGenerator : MonoBehaviour
{
    public GameObject deathZonePrefab; // DeathZone 프리팹
    public float spawnInterval = 3.0f; // DeathZone 생성 간격
    private Vector2 spawnRangeX = new Vector2(-33f, 33f); // X축 스폰 범위
    public float fixedYPosition = 0f; // 고정된 Y축 위치

    private void Start()
    {
        StartCoroutine(SpawnDeathZone());
    }

    private IEnumerator SpawnDeathZone()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // X축 위치는 랜덤, Y축 위치는 고정
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnRangeX.x, spawnRangeX.y),
                fixedYPosition
            );

            Instantiate(deathZonePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
