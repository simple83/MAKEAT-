using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    public static PlayUIManager instance;
    //GameManager와 동일한 싱글톤 패턴 생성. GameManager 주석 읽어보세용
    public static PlayUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayUIManager>();

                if (instance == null)
                {
                    Debug.Log("No Singletone instance");
                    Debug.Log("이거 나오면 망한거지 뭐... ㅋㅋ웃기네요-창현");
                }
            }
            return instance;
        }
    }
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
        /*중복 생성 방지. GameManager는 모든 씬을 관리하지만, PlayUIManager는 Play 씬만 관리하므로 
        새로운 씬 로딩시 PlayUIManager 파괴되도록 DonotDestroyOnLoad 안씁니다.*/
    }
    public enum Ingrediants //재료 열거형 자료. 코드 가독성 향상용.
    {
        Bread = 1,
        Tomato = 2,
        Cheese = 3,
        Ham = 4,
        Cabage = 5,
        Tortilla = 6
    }
    public enum Foods //음식 열거형 자료. 코드 가독성 향상용
    {
        Empty = 0,
        Sandwich = 1,
        Hamburger = 2,
        Pizza = 3
    }
    public Sprite[] ingrediantSpriteArray = new Sprite[7]; //재료 이미지 미리 로딩해두는 배열
    public Sprite[] foodsSpriteArray = new Sprite[4]; //음식 이미지 미리 로딩해두는 배열
    public Image[] InventoryUI; //재료 인벤토리 3개 UI 이미지 접근용
    public Image currentFoodUI; // 음식 인벤토리 UI 이미지 접근용
    private int index = 0; //지닌 재료 수
    private int[] ingreds = new int[3]; //현재 가진 재료 저장용 배열
    private Foods currentfood = 0; //현재 가진 음식 저장용 배열

    public void getIngrediant(Ingrediants ingred) 
    //재료획득 버튼 누를 시 실행되는 함수.
    {
        if (index < 3)//인벤토리가 꽉차지 않았을때. (0,1,2에 재료가 들어감.)
        {
            ingreds[index] = (int)ingred; //획득한 재료 인벤토리에 추가
            InventoryUI[index].sprite = ingrediantSpriteArray[ingreds[index]]; //인벤토리 UI 이미지도 변경
            GameManager.instance.ingredCount[ingreds[index]]++; //GamaManager에 재료 카운터에 정보전달
            index++;//인벤토리 인덱스 1 증가
            Debug.Log(index);
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            index = 3;//인덱스 오류 발생 방지용
        }

    }
    public void throwIngrediant()//재료 버리기
    {
        if(index > 0 && index < 4) //인벤토리 인덱스가 1,2,3일때.
        {
            index--; //인덱스 감소. 0,1,2번 인덱스에 재료가 들어있다.
            GameManager.instance.ingredCount[ingreds[index]]--;//재료 카운터에서 재료 제거
            ingreds[index] = 0;//인벤토리에서 재료 제거
            InventoryUI[index].sprite = ingrediantSpriteArray[0];//UI에서도 재료 제거
        }
        else if (index <= 0)
        {
            Debug.Log("인벤토리가 비어있습니다.");
            index = 0;//인덱스 오류 발생 방지용
        }
        else
        {
            Debug.Log("인벤토리 오류발생ㅠㅠ");
            //혹시 몰라서 예외처리
        }
    }

    public void MakeFood()
    {
        if(index < 3)
        {
            Debug.Log("재료가 부족합니다.");
        }
        else if(index == 3)
        {
            if (GameManager.instance.ingredCount[1] > 0 && GameManager.instance.ingredCount[2] > 0 && GameManager.instance.ingredCount[5] > 0)
            {
                Debug.Log("샌드위치 제작 시작");
                StartCoroutine("MakeSandwich");
            }
            else if (GameManager.instance.ingredCount[1] > 0 && GameManager.instance.ingredCount[3] > 0 && GameManager.instance.ingredCount[4] > 0)
            {
                Debug.Log("햄버거 제작 시작");
                StartCoroutine("MakeHamburger");
            }
            else if (GameManager.instance.ingredCount[2] > 0 && GameManager.instance.ingredCount[3] > 0 && GameManager.instance.ingredCount[6] > 0)
            {
                Debug.Log("피자 제작 시작");
                StartCoroutine("MakePizza");
            }
        }
    }
    IEnumerator MakeSandwich()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.ingredCount[1]--;
        GameManager.instance.ingredCount[2]--;
        GameManager.instance.ingredCount[5]--;
        Debug.Log("샌드위치 제작 완료");
        currentfood = Foods.Sandwich;
        currentFoodUI.sprite = foodsSpriteArray[(int)Foods.Sandwich];
    }
    IEnumerator MakeHamburger()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.ingredCount[1]--;
        GameManager.instance.ingredCount[3]--;
        GameManager.instance.ingredCount[4]--;
        Debug.Log("햄버거 제작 완료");
        currentfood = Foods.Hamburger;
        currentFoodUI.sprite = foodsSpriteArray[(int)Foods.Hamburger];
    }
    IEnumerator MakePizza()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.ingredCount[2]--;
        GameManager.instance.ingredCount[3]--;
        GameManager.instance.ingredCount[6]--;
        Debug.Log("피자 제작 완료");
        currentfood = Foods.Pizza;
        currentFoodUI.sprite = foodsSpriteArray[(int)Foods.Pizza];
    }
}
