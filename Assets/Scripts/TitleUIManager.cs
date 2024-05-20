using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
  void GameStartBtn()
    {
        SceneManager.LoadScene(1);
    }
}
