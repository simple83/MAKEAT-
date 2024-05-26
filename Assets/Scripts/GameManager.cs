using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //싱글톤 패턴 선언.
    //전역변수가 아니지만 다른 스크립트에서 GameManager의 변수에 접근할 수 있도록 해주는 것
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
    public bool isGameRunning = false; //게임 플레이 중 인지 체크
    public float runningTime = 0f; //게임 진행 시간
    public int[] ingredCount = {0,0,0,0,0,0,0};//0번은 비워두고, 1부터 빵. 재료 갯수 카운터
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);//씬 넘어갈때 오브젝트가 중복생성되지 않도록
        }
        DontDestroyOnLoad(this);//씬 넘어갈때 오브젝트 유지
        //모든 씬을 관리하는 단 1개의 GameManager를 만들기 위한 방법입니다.
    }
    public void GameStart() //게임시작 함수
    {
        isGameRunning = true; //게임 시작여부 플래그 true
        runningTime = 0; //게임 진행시간 초기화
        for (int i = 0; i < ingredCount.Length; i++)
        {
            ingredCount[i] = 0; // 재료 카운터 배열 초기화
        }
        SceneManager.LoadScene(1); //게임 시작씬으로 넘어가기 (Build setting 가면 씬 번호 있음)
    }
    public void GameOver()//게임 오버시 호출되는 함수
    {
        isGameRunning = false; //게임 진행 플래그 false
        runningTime = 0; //진행시간 초기화
        for (int i = 0; i < ingredCount.Length; i++)
        {
            ingredCount[i] = 0; // 재료 갯수 카운터 초기화
        }
        SceneManager.LoadScene(2); //게임오버 씬으로 전환
    }
}
