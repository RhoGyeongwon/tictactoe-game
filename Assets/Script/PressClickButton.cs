using System;
using UnityEngine;
using UnityEngine.UI;

public class PressClickButton : MonoBehaviour
{//여기서 반환값을 받아서 버튼에 적용시켜줘야하지 않을까?
    public Func<Image> buttonClickaction;
    private Image img;
    private Button button;
    private bool isClick = false;
    public bool IsClick
    {
        get => isClick;
    }

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void ButtonDown()
    {
        isClick = true;
    }
    
    public void ButtonUp()
    {
        img = buttonClickaction?.Invoke();
        button.image = img;
        button.enabled = false;
    }
}
