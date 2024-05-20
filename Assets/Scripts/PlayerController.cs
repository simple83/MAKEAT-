using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerMoveSpeed = 5.0f;
    public Slider saturationSlider;
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
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
    }
    public void PointerUpMoveLeft()
    {
        isMovingLeft = false;
    }

    public void PointerUpMoveRight()
    {
        isMovingRight = false;
    }

    public void PointerDownMoveLeft()
    {
        isMovingLeft = true;
    }

    public void PointerDownMoveRight()
    {
        isMovingRight = true;
    }
}
