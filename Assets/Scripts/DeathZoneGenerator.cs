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

            // DeathZone을 생성하고, DeathZoneController 기능을 수행할 컴포넌트 추가
            GameObject deathZone = Instantiate(deathZonePrefab, spawnPosition, Quaternion.identity);
            deathZone.AddComponent<DeathZoneController>(); // DeathZoneController 기능을 컴포넌트로 추가
        }
    }
}

public class DeathZoneController : MonoBehaviour
{
    private bool playerDetected = false; // 플레이어가 감지되었는지 여부
    private Coroutine destructionCoroutine; // 소멸 코루틴을 관리하기 위한 변수

    private void Start()
    {
        // 플레이어가 감지되지 않았을 경우 5초 후에 소멸
        destructionCoroutine = StartCoroutine(DestroyAfterTime(5.0f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true; // 플레이어 감지
            if (destructionCoroutine != null)
            {
                StopCoroutine(destructionCoroutine); // 기존 소멸 코루틴 중지
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerDetected)
            {
                // 플레이어가 떠난 후 2초 후에 소멸
                StartCoroutine(DestroyAfterTime(2.0f));
            }
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject); // 오브젝트 소멸
    }
}
