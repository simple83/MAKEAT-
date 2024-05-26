using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaturationSlider : MonoBehaviour
{
    public Slider saturationSlider;//포만감 슬라이더 접근용

    void Update()
    {
        if(GameManager.instance.isGameRunning)
        {
            float deltatime = Time.deltaTime;//시간에 따른 포만감 감소를 위해 시간 체크
            GameManager.instance.runningTime += deltatime;//게임매니저에 진행시간 전달
            if(GameManager.instance.runningTime < 60f && GameManager.instance.runningTime > 0) 
            {//진행시간이 0~60초의 경우
                saturationSlider.value = saturationSlider.value - (2.0f + (0.1333f) * deltatime) * deltatime;
            }
            else
            {
                saturationSlider.value = saturationSlider.value - 10f * deltatime; //60초 이후 고정
            }
            //게임종료
            if(saturationSlider.value <= 0)//슬라이더 값이 0이 될때
            {
                saturationSlider.value = 100f;//슬라이더 최대치 초기화
                GameManager.instance.GameOver();//게임오버 함수 호출
            }
        }
    }

}
