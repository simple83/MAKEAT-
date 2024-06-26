using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    public Button pickupButton;
    PlayUIManager playUImanager;
    private GameObject currentFood;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isfood = false;
        if (other.CompareTag("Bread"))
        {
            isfood = true;
            Debug.Log("Bread 접촉");
        }
        else if (other.CompareTag("Tomato")) {
            isfood = true;
            Debug.Log("Tomato 접촉");
        }
        else if (other.CompareTag("Cheese"))
        {
            isfood = true;
            Debug.Log("Cheese 접촉");
        }
        else if (other.CompareTag("Ham"))
        {
            isfood = true;
            Debug.Log("Ham 접촉");
        }
        else if (other.CompareTag("Cabbage"))
        {
            isfood = true;
            Debug.Log("Cabbage 접촉");
        }
        else if (other.CompareTag("Tortilla"))
        {
            isfood = true;
            Debug.Log("Tortilla 접촉");
        }

        if (isfood)
        {
            currentFood = other.gameObject;
            Debug.Log("재료획득 버튼 활성화");
            pickupButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentFood)
        {
            currentFood = null;
            Debug.Log("재료획득 버튼 비활성화");
            pickupButton.gameObject.SetActive(false);
        }
    }

    public void PickupItem()
    {
        if (currentFood != null && GameManager.instance.isCasting == false)
        {
            Vector3 point = currentFood.transform.position;
            Debug.Log(currentFood.tag + " 획득");
            if(currentFood.tag == "Bread")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Bread, point);
            }
            else if (currentFood.tag == "Tomato")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Tomato, point);
            }
            else if (currentFood.tag == "Cheese")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Cheese, point);
            }
            else if (currentFood.tag == "Ham")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Ham, point);
            }
            else if (currentFood.tag == "Cabbage")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Cabbage, point);
            }
            else if (currentFood.tag == "Tortilla")
            {
                PlayUIManager.instance.getIngrediant(PlayUIManager.Ingrediants.Tortilla, point);
            }
            Destroy(currentFood);
            pickupButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("음식 재료가 null 입니다.");
        }
    }
}
