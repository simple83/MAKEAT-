using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                    Debug.Log("이거 나오면 망한거지 뭐...");
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
        StartCoroutine(WaitForGameManager()) ; // 게임매니저 생성 대기 후 필요한 참조를 복사해옵니다.
    }
    public enum Ingrediants //재료 열거형 자료. 코드 가독성 향상용.
    {
        Bread = 1,
        Tomato = 2,
        Cheese = 3,
        Ham = 4,
        Cabbage = 5,
        Tortilla = 6
    }
    public enum Foods //음식 열거형 자료. 코드 가독성 향상용
    {
        Empty = 0,
        Sandwich = 1,
        Hamburger = 2,
        Pizza = 3
    }
    public Sprite[] ingrediantSpriteArray = new Sprite[7];
    //재료 이미지 미리 로딩해두는 배열

    public Sprite[] foodsSpriteArray = new Sprite[4];
    //음식 이미지 미리 로딩해두는 배열

    public Image[] InventoryUI;
    //재료 인벤토리 3개 UI 이미지 접근용

    public Image currentFoodUI;
    // 음식 인벤토리 UI 이미지 접근용

    public Button[] buttons;

    public EffectSoundManager effectSoundManager;

    public GameObject saturationSlider;

    public int[] inventoryingredCount;
    //인벤토리 재료 갯수 카운터. GameManager에 선언된 배열의 참조를 복사해와서 사용합니다.

    public int[] mapIngredCount;
    //맵 재료 갯수 카운터. GameManager에 선언된 배열의 참조를 복사해와서 사용합니다.

    public int castednumber;
    //음식 제작된 횟수. GameManager에 선언된 변수의 참조를 복사해와서 사용합니다.

    public float castingTime;
    //캐스팅 시간.  GameManager에 선언된 변수의 참조를 복사해와서 사용합니다.


    private int index = 0;
    //지닌 재료 수.

    private int[] ingreds = new int[3];
    //현재 가진 재료 저장용 배열

    private Foods currentfood = 0;
    //현재 가진 음식 저장용 배열


    public void getIngrediant(Ingrediants ingred, Vector3 spawnPoint) 
    //재료획득 버튼 누를 시 실행되는 함수.
    {
        if (GameManager.instance.isCasting == false) //음식 제작중엔 안됨
        {
            if (index < 3)//인벤토리가 꽉차지 않았을때. (0,1,2에 재료가 들어감.)
            {
                ingreds[index] = (int)ingred; //획득한 재료 인벤토리에 추가
                InventoryUI[index].sprite = ingrediantSpriteArray[ingreds[index]]; //인벤토리 UI 이미지도 변경
                inventoryingredCount[ingreds[index]]++; //GamaManager 인벤토리 재료 카운터에 정보전달
                mapIngredCount[ingreds[index]]--;//GamaManager 맵 재료 카운터에 정보전달
                int pointindex = -1;
                for (int i = 0; i < IngrediantGenerator.Instance.spawnPoints.Length; i++)
                {
                    if (IngrediantGenerator.Instance.spawnPoints[i].Equals(spawnPoint))
                    {
                        pointindex = i;
                        break;
                    }
                }
                if (pointindex == -1)
                {
                    Debug.Log("재료 좌표오류");
                }
                else
                {
                    IngrediantGenerator.Instance.isSpawnPointSelected[pointindex] = false;
                    //생성 가능한 좌표 목록 업데이트
                }
                index++;//인벤토리 인덱스 1 증가
                Debug.Log(index);
                effectSoundManager.PickUpSound(buttons[1].GetComponent<AudioSource>());//버튼 효과음
            }
            else
            {
                Debug.Log("인벤토리가 가득 찼습니다.");
                index = 3;//인덱스 오류 발생 방지용
            }
            if (index == 3)
            {
                ButtonImageChanger buttonImageChanger = buttons[0].GetComponent<ButtonImageChanger>();
                if (inventoryingredCount[1] > 0 && inventoryingredCount[2] > 0 && inventoryingredCount[5] > 0)
                {
                    Debug.Log("샌드위치 제작가능");
                    buttonImageChanger.ButtonDefault();
                    currentFoodUI.sprite = foodsSpriteArray[0];
                }
                else if (inventoryingredCount[1] > 0 && inventoryingredCount[3] > 0 && inventoryingredCount[4] > 0)
                {
                    Debug.Log("햄버거 제작가능");
                    buttonImageChanger.ButtonDefault();
                    currentFoodUI.sprite = foodsSpriteArray[0];
                }
                else if (inventoryingredCount[2] > 0 && inventoryingredCount[3] > 0 && inventoryingredCount[6] > 0)
                {
                    Debug.Log("피자 제작가능");
                    buttonImageChanger.ButtonDefault();
                    currentFoodUI.sprite = foodsSpriteArray[0];
                }
            }
        }

    }
    public void throwIngrediant()//재료 버리기
    {
        if (GameManager.instance.isCasting == false) //음식 제작중엔 안됨
        {
            if (index > 0 && index < 4) //인벤토리 인덱스가 1,2,3일때.
            {
                index--; //인덱스 감소. 0,1,2번 인덱스에 재료가 들어있다.
                inventoryingredCount[ingreds[index]]--;//재료 카운터에서 재료 제거
                ingreds[index] = 0;//인벤토리에서 재료 제거
                InventoryUI[index].sprite = ingrediantSpriteArray[0];//UI에서도 재료 제거
                effectSoundManager.ThrowSound(buttons[2].GetComponent<AudioSource>());
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
    }

    public void MakeFood()
    {
        if (GameManager.instance.isCasting == false) // 음식 제작중엔 안됨
        {
            if (index < 3)
            {
                Debug.Log("재료가 부족합니다.");
            }
            else if (index == 3)
            {
                if (inventoryingredCount[1] > 0 && inventoryingredCount[2] > 0 && inventoryingredCount[5] > 0)
                {
                    Debug.Log("샌드위치 제작 시작");
                    StartCoroutine(MakeSandwich());             
                }
                else if (inventoryingredCount[1] > 0 && inventoryingredCount[3] > 0 && inventoryingredCount[4] > 0)
                {
                    Debug.Log("햄버거 제작 시작");
                    StartCoroutine(MakeHamburger());
                }
                else if (inventoryingredCount[2] > 0 && inventoryingredCount[3] > 0 && inventoryingredCount[6] > 0)
                {
                    Debug.Log("피자 제작 시작");
                    StartCoroutine(MakePizza());
                }

                effectSoundManager.MakeFoodSound(buttons[0].GetComponent<AudioSource>()); 
            }
        }
    }
    public void EatFood()
    {
        ButtonImageChanger buttonImageChanger = buttons[0].GetComponent<ButtonImageChanger>();
        if (currentfood == Foods.Sandwich)
        {
            Slider slider = saturationSlider.GetComponent<Slider>();
            slider.value += 20;
            buttonImageChanger.ButtonDisabled();
            currentFoodUI.sprite = foodsSpriteArray[0];
            currentfood = Foods.Empty;
            effectSoundManager.EatFoodSound(buttons[0].GetComponent<AudioSource>());
        }
        else if (currentfood == Foods.Hamburger)
        {
            Slider slider = saturationSlider.GetComponent<Slider>();
            slider.value += 30;
            buttonImageChanger.ButtonDisabled();
            currentFoodUI.sprite = foodsSpriteArray[0];
            currentfood = Foods.Empty;
            effectSoundManager.EatFoodSound(buttons[0].GetComponent<AudioSource>());
        }
        else if (currentfood == Foods.Pizza)
        {
            Slider slider = saturationSlider.GetComponent<Slider>();
            slider.value += 40;
            buttonImageChanger.ButtonDisabled();
            currentFoodUI.sprite = foodsSpriteArray[0];
            currentfood = Foods.Empty;
            effectSoundManager.EatFoodSound(buttons[0].GetComponent<AudioSource>());
        }
        else
        {
            Debug.Log("음식없음");
        }
    }
    public void castedNumberUpdate()
    {
        castednumber++;
        if (castednumber < 10)
        {
            castingTime = castingTime - 0.1f;
        }
        else if (castednumber >= 10)
        {
            castingTime = 1f;
        }
        else
        {
            Debug.Log("캐스팅 횟수 오류발생");
        }
    }

    //아래는 코루틴을 사용해 대기시간이나 쿨타임을 구현하는 방식입니다.
    //IEnumerator로 함수처럼 내용을 선언하면 됩니다. 
    //호출은 StartCouroutine("MakeSandwich"); 이런식으로
    IEnumerator MakeSandwich()
    {
        ButtonImageChanger buttonImageChanger = buttons[0].GetComponent<ButtonImageChanger>();
        buttonImageChanger.ButtongWorking();
        GameManager.instance.isCasting = true; // 캐스팅 시작
        yield return new WaitForSeconds(castingTime); // 캐스팅 시간만큼 대기
        inventoryingredCount[1]--;
        inventoryingredCount[2]--;
        inventoryingredCount[5]--;
        for (int i = 0; i < 3; i++) {
            InventoryUI[i].sprite = ingrediantSpriteArray[0];
        }
        index = 0;
        //인벤토리에서 재료 사용
        Debug.Log("샌드위치 제작 완료");
        buttonImageChanger.ButtonDefault();
        currentfood = Foods.Sandwich; //음식 인벤토리에 추가
        currentFoodUI.sprite = foodsSpriteArray[(int)Foods.Sandwich]; // 음식 UI 변경
        GameManager.instance.isCasting = false; // 캐스팅 종료
        castedNumberUpdate();
        effectSoundManager.MakeFoodSound(buttons[0].GetComponent<AudioSource>());
    }
    IEnumerator MakeHamburger()
    {
        ButtonImageChanger buttonImageChanger = buttons[0].GetComponent<ButtonImageChanger>();
        buttonImageChanger.ButtongWorking();
        GameManager.instance.isCasting = true;
        yield return new WaitForSeconds(castingTime);
        inventoryingredCount[1]--;
        inventoryingredCount[3]--;
        inventoryingredCount[4]--;
        for (int i = 0; i < 3; i++)
        {
            InventoryUI[i].sprite = ingrediantSpriteArray[0];
        }
        index = 0;
        Debug.Log("햄버거 제작 완료");
        buttonImageChanger.ButtonDefault();
        currentfood = Foods.Hamburger;
        currentFoodUI.sprite = foodsSpriteArray[(int)Foods.Hamburger];
        GameManager.instance.isCasting = false;
        castedNumberUpdate();
        effectSoundManager.MakeFoodSound(buttons[0].GetComponent<AudioSource>());
    }
    IEnumerator MakePizza()
    {
        ButtonImageChanger buttonImageChanger = buttons[0].GetComponent<ButtonImageChanger>();
        buttonImageChanger.ButtongWorking();
        GameManager.instance.isCasting = true;
        yield return new WaitForSeconds(castingTime);
        inventoryingredCount[2]--;
        inventoryingredCount[3]--;
        inventoryingredCount[6]--;
        for (int i = 0; i < 3; i++)
        {
            InventoryUI[i].sprite = ingrediantSpriteArray[0];
        }
        index = 0;
        Debug.Log("피자 제작 완료");
        buttonImageChanger.ButtonDefault();
        currentfood = Foods.Pizza;
        currentFoodUI.sprite = foodsSpriteArray[(int)Foods.Pizza];
        GameManager.instance.isCasting = false;
        castedNumberUpdate();
        effectSoundManager.MakeFoodSound(buttons[0].GetComponent<AudioSource>());
    }

    private IEnumerator WaitForGameManager()
    {
        yield return new WaitUntil(() => GameManager.instance);
        //GameManager 인스턴스가 생성되기를 기다립니다.

        inventoryingredCount = GameManager.instance.inventoryIngredCount;
        //인벤토리 재료 갯수 카운터. GameManager 인스턴스 생성 이후 참조복사

        mapIngredCount = GameManager.instance.mapIngredCount;
        //맵 재료 갯수 카운터

        castednumber = GameManager.instance.castednumber;
        //음식 제작된 횟수

        castingTime = GameManager.instance.castingTime;
        //캐스팅 시간
}
}
