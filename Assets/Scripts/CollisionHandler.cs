using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    public Button pickupButton;
    PlayUIManager playUImanager;
    private GameObject currentFood;
    public float deathTime = 3.0f; // 플레이어가 사망하는데 걸리는 시간
    private GameObject DeathZoneObject; // 플레이어가 체류된 DeathZone 오브젝트를 저장하기 위한 변수
    static private float timer = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isfood = false;
        if (other.CompareTag("Bread"))
        {
            isfood = true;
            Debug.Log("Bread 접촉");
        }
        else if (other.CompareTag("Tomato"))
        {
            isfood = true;
            Debug.Log("Tomato 접촉");
        }
        else if (other.CompareTag("Cheese"))
        {
            isfood = true;
            Debug.Log("Cheese 접촉");
        }
        else if (other.CompareTag("Ham"))
        {
            isfood = true;
            Debug.Log("Ham 접촉");
        }
        else if (other.CompareTag("Cabbage"))
        {
            isfood = true;
            Debug.Log("Cabbage 접촉");
        }
        else if (other.CompareTag("Tortilla"))
        {
            isfood = true;
            Debug.Log("Tortilla 접촉");
        }

        if (isfood)
        {
            currentFood = other.gameObject;
            Debug.Log("재료획득 버튼 활성화");
            pickupButton.gameObject.SetActive(true);
        }


        if (other.CompareTag("Fall")) //낙사
        {
            GameManager.instance.GameOver();//게임오버 함수 호출
        }

        if (other.CompareTag("DeathZone"))
        {
            DeathZoneObject = other.gameObject;// 감지된 데스존 오브젝트 저장
            Debug.Log("DeathZone 접촉");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentFood)
        {
            currentFood = null;
            Debug.Log("재료획득 버튼 비활성화");
            pickupButton.gameObject.SetActive(false);
        }

        if (other.CompareTag("DeathZone"))
        {
            DeathZoneObject = null; // 플레이어 오브젝트 초기화
            timer = 0f; // 타이머 초기화
            Debug.Log("DeathZone 해제");
        }
    }

    public void PickupItem()
    {
        if (currentFood != null && GameManager.instance.isCasting == false)
        {
            Vector3 point = currentFood.transform.position;
            Debug.Log(currentFood.tag + " 획득");
            if (currentFood.tag == "Bread")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Bread, point);
            }
            else if (currentFood.tag == "Tomato")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Tomato, point);
            }
            else if (currentFood.tag == "Cheese")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Cheese, point);
            }
            else if (currentFood.tag == "Ham")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Ham, point);
            }
            else if (currentFood.tag == "Cabbage")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Cabbage, point);
            }
            else if (currentFood.tag == "Tortilla")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Tortilla, point);
            }
            Destroy(currentFood);
            pickupButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("음식 재료가 null 입니다.");
        }
    }

    private void Update()
    {
        //데스존 사망 트리거
        if (DeathZoneObject != null) // 저장된 데스존 오브젝트가 있을 때만 실행
        {
            timer += Time.deltaTime; // 머문 시간 측정
            if (timer >= deathTime)
            {
                GameManager.instance.GameOver(); //게임오버 함수 호출
                Debug.Log("Player Wasted");
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other) //낙사
    {
        if (other.gameObject.CompareTag("Fall")) //Fall(낙사) tag 오브젝트와 충돌시 호출
        {
            GameManager.instance.GameOver();//게임오버 함수 호출

            Debug.Log("Player 낙사");
        }
    }
}
