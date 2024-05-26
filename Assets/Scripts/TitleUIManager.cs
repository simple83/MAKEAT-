using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public void GameStartBtn()
    {
         GameManager.instance.GameStart();//게임 시작 버튼
    }

    public void GoTitleBtn()
    {
        SceneManager.LoadScene(0);//타이틀로 가는 버튼
    }
}
