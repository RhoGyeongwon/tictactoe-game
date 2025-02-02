using System;
using UnityEngine;
using UnityEngine.UI;

public class PressClickButton : MonoBehaviour
{//여기서 반환값을 받아서 버튼에 적용시켜줘야하지 않을까?
    public Func<Sprite> buttonClickaction;
    private Sprite buttonSprite;
    private Button gameButton;
    private bool isClick = false;

    void Start()
    {
        gameButton = GetComponent<Button>();
    }

    public void ButtonUp()
    {
        Debug.Log("Click");
        if (!isClick)
        {
            buttonClickaction = () => GameManager.Instance.PressTurnClick();
            gameButton.image.sprite = buttonClickaction?.Invoke();
            isClick = true;
            gameButton.enabled = false;
        }
    }
}
