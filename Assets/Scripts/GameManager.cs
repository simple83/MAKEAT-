using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            { 
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    Debug.Log("No Singletone instance");
                    Debug.Log("이거 나오면 망한거지 뭐...");
                }
            }
            return instance;
        }
    }
    public bool isGameRunning = true;
    public float runningTime = 0f; //게임 진행 시간
    public int[] ingredCount = {0,0,0,0,0,0,0};//0번은 비워두고, 1부터 빵
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
