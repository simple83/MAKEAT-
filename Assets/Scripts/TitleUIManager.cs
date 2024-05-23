using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public void GameStartBtn()
    {
         GameManager.instance.GameStart();
    }

    public void GoTitleBtn()
    {
        SceneManager.LoadScene(0);
    }
}
