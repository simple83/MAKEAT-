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

    public void getIngrediant(Ingrediants ingred)
    {
        if (index < 3)
        {
            InventoryUI[index].sprite = ingrediantSpriteArray[(int)ingred];
            index++;
            Debug.Log(index);
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            index = 2;
        }

    }
    public void throwIngrediant()
    {
        if(index > 0 && index < 3)
        {
            index--;
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

}
