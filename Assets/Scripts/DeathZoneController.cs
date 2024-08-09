using System.Collections;
using UnityEngine;

public class DeathZoneController : MonoBehaviour
{
    private bool playerDetected = false; // 플레이어가 감지되었는지 여부
    private bool playerIsHidden = false; // 플레이어가 HideZone에 완전히 숨겨졌는지 여부
    private Collider2D deathZoneCollider; // DeathZone의 Collider2D
    private Coroutine destructionCoroutine; // 소멸 코루틴을 관리하기 위한 변수

    private void Start()
    {
        deathZoneCollider = GetComponent<Collider2D>(); // DeathZone의 Collider2D 가져오기
        destructionCoroutine = null; // 초기화
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true; // 플레이어 감지
            if (destructionCoroutine != null)
            {
                StopCoroutine(destructionCoroutine); // 기존 소멸 코루틴 중지
                destructionCoroutine = null; // 소멸 코루틴 변수 초기화
            }
        }

        if (other.CompareTag("HideZone"))
        {
            // HideZone의 Collider2D
            Collider2D hideZoneCollider = other.GetComponent<Collider2D>();
            if (hideZoneCollider != null && deathZoneCollider != null)
            {
                // HideZone이 DeathZone의 Collider2D 범위 내에 완전히 포함되면
                if (IsWithinBounds(hideZoneCollider.bounds, deathZoneCollider.bounds))
                {
                    playerIsHidden = true; // HideZone 내에서 숨김
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerDetected)
            {
                // 플레이어가 DeathZone을 떠났을 때, 2초 후 소멸 코루틴 시작
                if (destructionCoroutine != null)
                {
                    StopCoroutine(destructionCoroutine); // 기존 소멸 코루틴 중지
                }
                destructionCoroutine = StartCoroutine(DestroyAfterTime(2.0f)); // 2초 후 소멸 코루틴 시작
                playerDetected = false; // 플레이어가 DeathZone을 떠났으므로 감지 상태 초기화
            }
        }

        if (other.CompareTag("HideZone"))
        {
            playerIsHidden = false; // 플레이어가 HideZone에서 나왔을 때
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // 플레이어가 감지되었고, HideZone에 숨지 않은 상태에서만 GameOver 호출
        if (playerDetected && !playerIsHidden)
        {
            GameManager.instance.GameOver();
            Debug.Log("Player Wasted");
        }

        Destroy(gameObject); // 오브젝트 소멸
    }

    //Player가 HideZone에 완전히 몸을 숨겼는지 확인하는 함수
    private bool IsWithinBounds(Bounds bounds1, Bounds bounds2)
    {
        return bounds1.min.x >= bounds2.min.x &&
               bounds1.min.y >= bounds2.min.y &&
               bounds1.max.x <= bounds2.max.x &&
               bounds1.max.y <= bounds2.max.y;
    }
}
