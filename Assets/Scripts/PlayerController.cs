using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerMoveSpeed = 5.0f; //플레이어 이동속도
    public float playerJumpForce = 10.0f; //플레이어 점프력
    public float gravity = -9.8f; //중력 설정
    public Slider saturationSlider; // 포만감 슬라이더 접근용
    public bool isMovingLeft = false; //플레이어가 왼쪽 이동중인지 체크
    public bool isMovingRight = false;//플레이어가 오른쪽 이동중인지 체크
    public bool isJump = false; //플레이어가 점프했는지 체크
    public int jumpCount = 2; //플레이어 점프 가능 횟수

    public Rigidbody2D playerRigidbody;

    void Start()
    {
        // Rigidbody2D 컴포넌트를 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (GameManager.instance.isCasting == false) 
        {//음식 제작 도중엔 이동 불가능
            if (isMovingLeft)
            {
                // 왼쪽으로 이동
                transform.Translate(Vector3.left * playerMoveSpeed * Time.deltaTime);
            }

            if (isMovingRight)
            {
                // 오른쪽으로 이동
                transform.Translate(Vector3.right * playerMoveSpeed * Time.deltaTime);
            }           
        }
        saturationSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
        //포만감 슬라이더가 플레이어를 따라가도록.
    }
    public void PointerUpMoveLeft()//MoveLeft버튼을 누르고 있을 때
    {
        isMovingLeft = false;
    }

    public void PointerUpMoveRight()//MoveRight버튼을 누르고 있을 때
    {
        isMovingRight = false;
    }

    public void PointerDownMoveLeft()//MoveLeft버튼을 떼는 순간
    {
        isMovingLeft = true;
    }

    public void PointerDownMoveRight()//MoveRight버튼을 떼는 순간
    {
        isMovingRight = true;
    }

    public void OnClickJump()//Jump버튼 누름
    {

        if (!isJump && jumpCount == 2)//1단점프
        {
            jumpCount--;
            playerRigidbody.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
        }
        else if(!isJump && jumpCount == 1)//2단점프, 현재 종축 속력 0으로 초기화 후 가속 추가.
        {
            Debug.Log("2단점프 시도중");
            jumpCount--;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
            isJump = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿으면 점프 카운터 초기화
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
            isJump = false;
        }
    }
}
