using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    public static PlayUIManager instance;
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
    }
    public enum Ingrediants
    {
        Bread = 1,
        Tomato = 2,
        Cheese = 3,
        Ham = 4,
        Cabage = 5,
        Tortilla = 6
    }
    public Sprite[] ingrediantSpriteArray = new Sprite[7];
    public Image[] InventoryUI;
    private int index = 0;
    private int[] ingreds = new int[3];

    public void getIngrediant(Ingrediants ingred)
    {
        if (index < 3)
        {
            ingreds[index] = (int)ingred;
            InventoryUI[index].sprite = ingrediantSpriteArray[ingreds[index]];
            GameManager.instance.ingredCount[ingreds[index]]++;
            index++;
            Debug.Log(index);
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            index = 3;
        }

    }
    public void throwIngrediant()
    {
        if(index > 0 && index < 4)
        {
            index--;
            GameManager.instance.ingredCount[ingreds[index]]--;
            InventoryUI[index].sprite = ingrediantSpriteArray[0];
        }
        else if (index <= 0)
        {
            Debug.Log("인벤토리가 비어있습니다.");
            index = 0;
        }
        else
        {
            Debug.Log("인벤토리 오류발생ㅠㅠ");
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
        }
    }
    IEnumerator MakeSandwich()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.ingredCount[1]--;
        GameManager.instance.ingredCount[2]--;
        GameManager.instance.ingredCount[5]--;
        Debug.Log("샌드위치 제작 완료");
        //음식 인벤토리 추가 필요
    }
}
