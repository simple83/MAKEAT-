using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerMoveSpeed = 5.0f; //플레이어 이동속도
    public Slider saturationSlider; // 포만감 슬라이더 접근용
    public bool isMovingLeft = false; //플레이어가 왼쪽 이동중인지 체크
    public bool isMovingRight = false;//플레이어가 오른쪽 이동중인지 체크
    void Update()
    {
        if(isMovingLeft)
        {
            // 왼쪽으로 이동
            transform.Translate(Vector3.left * playerMoveSpeed * Time.deltaTime);
        }

        if (isMovingRight)
        {
            // 오른쪽으로 이동
            transform.Translate(Vector3.right * playerMoveSpeed * Time.deltaTime);
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
}
