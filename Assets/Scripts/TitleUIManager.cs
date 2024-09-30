using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TitleUIManager : MonoBehaviour
{
    public GameObject HTP1;
    public GameObject HTP2;
    public GameObject HTP1Btn;
    public GameObject HTP2Btn;
    public GameObject HTP3Btn;
    public void GameStartBtn()
    {
         GameManager.instance.GameStart();//게임 시작 버튼
    }

    public void GoTitleBtn()
    {
        SceneManager.LoadScene(0);//타이틀로 가는 버튼
    }

    public void HowToPlay1Btn()
    {
        HTP1.SetActive(true);
        HTP1Btn.SetActive(false);
        HTP2Btn.SetActive(true);
    }

    public void HowToPlay2Btn()
    {
        HTP2.SetActive(true);
        HTP2Btn.SetActive(false);
        HTP3Btn.SetActive(true);
    }

    public void HowToPlay3Btn()
    {
        HTP1.SetActive(false);
        HTP2.SetActive(false);
        HTP3Btn.SetActive(false);
        HTP1Btn.SetActive(true);
    }
}
