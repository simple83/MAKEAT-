using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaturationSlider : MonoBehaviour
{
    public Slider saturationSlider;

    void Update()
    {
        if(GameManager.instance.isGameRunning)
        {
            float deltatime = Time.deltaTime;
            GameManager.instance.runningTime += deltatime;
            if(GameManager.instance.runningTime < 60f && GameManager.instance.runningTime > 0) {
                saturationSlider.value = saturationSlider.value - (2.0f + (0.1333f) * deltatime) * deltatime;
            }
            else
            {
                saturationSlider.value = saturationSlider.value - 10f * deltatime; //60초 이후 고정
            }
            //게임종료
            if(saturationSlider.value <= 0)
            {
                saturationSlider.value = 100f;
                GameManager.instance.GameOver();
            }
        }
    }

}
