using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class ButtonImageChanger : MonoBehaviour
{
    public Sprite[] buttonImages;
    // 0번 디폴트, 1번 클릭된상태, 2번 비활성화, 3번 버튼클릭불가(사용중)
    public Image currentButtonImage;

    public void ButtonDefault()
    {
        currentButtonImage.sprite = buttonImages[0];
    }
    public void ButtonClicked()
    {
        currentButtonImage.sprite = buttonImages[1];
    }
    public void ButtonDisabled()
    {
        currentButtonImage.sprite = buttonImages[2];
    }
    public void ButtongWorking()
    {
        currentButtonImage.sprite = buttonImages[3];
    }
}
